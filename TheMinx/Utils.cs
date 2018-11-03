using System;
using System.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using TheMinx.Tatts.Data.Requests;
using TheMinx.Tatts.Data.Responses;
using TheMinx.wsTabAuth;
using TheMinx.wsTabRacing;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Data;
using System.Timers;
using System.Threading;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;
using System.Net;
using System.Xml;

namespace TheMinx
{
    public class Utils
    {
        Globals global = new Globals();
        public Telerik.WinControls.UI.RadProgressBar prog;
        public ToolStripStatusLabel ts;
        public string RatingsFile;
        public Label ntgLabel;
        public DataGridView dgRace;
        string lastRace = "";
        PriceUpdates upd;
        System.Timers.Timer ntgTimer;
        public ComboBox cmbVenue, cmbRace;
        public KryptonRichTextBox txtStatus, ntgRacestxt;
        private int running;

        public bool pctOn { get; set; }
        public bool useRatingBetAmount { get; set; }
        public bool betOnVic { get; set; }
        public bool betOnNsw { get; set; }
        public bool betOnQld { get; set; }
        public bool betOnAll { get; set; }
        public double betPct { get; set; }
        public double betToWin { get; set; }

        int ticks = 0;
        public Utils()
        {}

        public void LoadData()
        {
            global.dbCon.Open();

            RacingServiceClient raceServ = new RacingServiceClient();
            wsTabRacing.apiMeta rMeta = new wsTabRacing.apiMeta();
            rMeta.usernamePasswordToken = Globals.DefaultAuth;
            rMeta.requestChannel = Globals.VicChan;
            rMeta.jurisdictionId = Globals.VicJury;
            rMeta.deviceId = Globals.DeviceID;
            rMeta.deviceIdSpecified = true;

            meetingsRequest mreq = new meetingsRequest();
            meetingsResponse mresp = new meetingsResponse();
            mreq.racingCode = "G";
            mresp = raceServ.getMeetings(rMeta, mreq);
            foreach (meeting1 meet in mresp.meeting)
            {
                prog.Value1 = ticks;
                meetingDetailsRequest mDetReq = new meetingDetailsRequest();
                meetingDetailsResponse mDetResp = new meetingDetailsResponse();
                mDetReq.meetingDateIdentifier = meet.meetingDateString;
                mDetReq.meetingName = meet.meetingName;
                mDetReq.racingCode = meet.racingCode;
                mDetResp = raceServ.getMeetingDetails(rMeta, mDetReq);

                if (mDetResp.meeting.raceDetailList != null)
                {
                    foreach (raceDetail rdet in mDetResp.meeting.raceDetailList)
                    {
                        string raceID = meet.meetingIdentifier + "." + rdet.raceNumber;
                        string venue = meet.meetingName;
                        if (venue == "MOUNT GAMBIER") venue = "MT GAMBIER";
                        int racenum = Convert.ToInt32(rdet.raceNumber);
                        string jump = rdet.raceStartTime.ToString("HH:mm:ss");
                        string raceDate = meet.meetingDate.ToString("yyyyMMdd");
                        raceDate = DateTime.Now.ToString("yyyyMMdd");

                        raceSelectionDetail[] rRunners = rdet.raceSelections;
                        foreach (raceSelectionDetail runner in rRunners)
                        {
                            int box = (int)runner.number;
                            string name = runner.runner;
                            name = name.Replace("'", "");
                            string runnerNumber = runner.barrierNumber.ToString();
                            string status = runner.status;
                            string upd = "insert into LivePrices (RaceID, RaceDate, Box, RaceTime, Runner, RunnerNumber, MeetingCode, RaceNum, Venue, QldID, VicID, NswID, Status) values ('" + raceID + "','" + raceDate + "'," + box + ",'" + jump + "','" + name + "','" + runnerNumber + "','" + meet.meetingCode + "','" + rdet.raceNumber + "','" + venue + "','" + "" + "','" + raceID + "','" + raceID + "','" + status + "')";
                            SqlCommand cmdupd = new SqlCommand(upd, global.dbCon);
                            try
                            {
                                if (global.dbCon.State == System.Data.ConnectionState.Closed) global.dbCon.Open();
                                cmdupd.ExecuteNonQuery();
                            }
                            catch (Exception e1)
                            {
                                Console.WriteLine(e1.Message.ToString());
                            }
                            ts.Text = "Loaded: " + runner.runner;
                        }
                        ticks++;
                        if (ticks == 100) ticks = 0;
                    }
                }
            }

            ticks = 0;
            string qldDate = DateTime.Now.ToString("yyyy/MM/dd");
            string meetJSON = TattsUtils.CallAPI("data/racing/" + qldDate + "/", null, true, "GET");
            DataResponse data = JsonConvert.DeserializeObject<DataResponse>(meetJSON);
            MeetingSummary[] meetsum = data.Data.ToArray();
            Meeting[] meetings = meetsum[0].Meetings.ToArray();
            foreach (Meeting meeting in meetings)
            {
                prog.Value1 = ticks;
                if (meeting.MeetingType == "G")
                {
                    TattsRace[] races = meeting.Races.ToArray();
                    foreach (TattsRace race in races)
                    {
                        string raceDate = DateTime.Now.ToString("yyyyMMdd");
                        string hdDate = DateTime.Now.ToString("dd-MMM-yy");
                        string meetingCode = meeting.MeetingCode;
                        string raceID = "data/racing/" + qldDate + "/" + meetingCode + "/" + race.RaceNumber + "/";
                        string responseJSON = TattsUtils.CallAPI(raceID, null, true, "GET");
                        string QldID = meetingCode + "_" + meeting.MeetingType + "_" + race.RaceNumber;
                        if (QldID == "")
                        {
                            Console.WriteLine("here");
                        }

                        DataResponse response = null;
                        response = JsonConvert.DeserializeObject<DataResponse>(responseJSON);
                        string venue = response.Data[0].Meetings[0].VenueName.ToUpper();
                        int? racenum = race.RaceNumber;
                        string upd = "update LivePrices set QldID = '" + raceID + "', QLDMeetingCode = '" + meetingCode + "' where RaceDate = '" + raceDate + "' and Venue = '" + venue + "' and RaceNum = '" + racenum + "'";
                        SqlCommand cmdupd = new SqlCommand(upd, global.dbCon);
                        try
                        {
                            if (global.dbCon.State == System.Data.ConnectionState.Closed) global.dbCon.Open();
                            cmdupd.ExecuteNonQuery();
                        }
                        catch (Exception e1)
                        {
                            Console.WriteLine(e1.Message.ToString());
                        }
                        ts.Text = "Loaded: " + venue;
                        Console.WriteLine("Loaded: " + venue);
                        ticks++;
                        if (ticks == 100) ticks = 0;
                    }
                }
            }

            global.dbCon.Close();
//            prog.Stop();
            prog.Invoke(new MethodInvoker(delegate { prog.Visible = false; }));
            ts.Text = "Todays Data Loaded";
            Globals.TodayLoaded = true;
            DialogResult rslt = MessageBox.Show("The Minx will now close to refresh loaded data. Please start application again.", "Info...", MessageBoxButtons.OK);
            if (rslt == DialogResult.OK) Application.Exit();
        }

        public void LoadRatings()
        {
            global.dbCon.Open();

            string updInfo = "update LivePrices set Tip = '', MyPrice = '', BetAmt = '' where RaceDate = '" + DateTime.Now.ToString("yyyyMMdd") + "'";
            SqlCommand upd = new SqlCommand(updInfo, global.dbCon);
            upd.ExecuteNonQuery();

            try
            {
                using (CsvReader csv = new CsvReader(new StreamReader(RatingsFile), true))
                {
                    int fieldcount = csv.FieldCount;
                    string[] headers = csv.GetFieldHeaders();
                    try
                    {
                        while (csv.ReadNextRecord())
                        {
                            string track = csv[0];
                            string race = csv[2];
                            string box = csv[4];
                            string dog = csv[5];
                            dog = dog.Replace("'", "");
                            string price = csv[6];
                            string bet = csv[7];
                            string tip = csv[8];
                            if (track == "SANDOWN PARK") { track = "SANDOWN"; }
                            if (track == "MOUNT GAMBIER") { track = "MT GAMBIER"; }
                            if (track.Contains("ELWICK")) { track = "ELWICK (HOB)"; }
                            if (track.Contains("HOBART")) { track = "ELWICK (HOB)"; }
                            if (tip != "")
                            {
                                updInfo = "update LivePrices set Tip = '" + tip + "', MyPrice = '" + price + "', BetAmt = '" + bet + "' where RaceDate = '" + DateTime.Now.ToString("yyyyMMdd") + "' and Box = '" + box + "' and RaceNum = '" + race + "' and Venue like '%" + track + "%'";
                                upd = new SqlCommand(updInfo, global.dbCon);
                                upd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Error in Import File. Please correct file and try again." + err.Message.ToString(), "Error", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception fex)
            {
                MessageBox.Show("Error: " + fex.Message, "Error", MessageBoxButtons.OK);
            }

            global.dbCon.Close();

//            prog.Stop();
            prog.Invoke(new MethodInvoker(delegate { prog.Visible = false; }));
        }

        public void Login()
        {
            #region Login QLD
            if (Globals.QldEnable)
            {
                LogonRequest request = new LogonRequest();
                request.Username = Globals.QldAcc.ToString();
                request.Password = Globals.QldPwd;
                string requestJSON = JsonConvert.SerializeObject(request);
                string responseJSON = TattsUtils.CallAPI2("account/login/", requestJSON, true, "POST");
//                responseJSON = responseJSON.Substring(0, 70) + @"}}";
//                responseJSON = "{\"CustomerSession\":{\"SessionId\":\"fe2e7036-4d1b-401c-8b14-28a0855010b5\"},\"CustomerReference\":\"50177979\",\"Jurisdiction\":\"AustraliaVIC\",\"FirstName\":\"Dean\",\"LastLogin\":\"2014-10-24T04:00:00\",\"Balance\":{\"SharedBalance\":0.94}}";
                LogonResponse response = null;
//                string tst = responseJSON.Substring(195, 5);
                if (!string.IsNullOrEmpty(responseJSON))
                {
                    response = JsonConvert.DeserializeObject<LogonResponse>(responseJSON, new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore,
                            Formatting = Newtonsoft.Json.Formatting.None,
                            DateFormatHandling = DateFormatHandling.IsoDateFormat,
                            FloatParseHandling = FloatParseHandling.Decimal,
                        });
                }

                if (response != null)
                {
                    if (response.Success)
                    {
                        Globals.QldAuth = response.CustomerSession.SessionId;
                        WriteLog("QLD Login Ok: " + response.Message + "-" + response.CustomerSession.SessionId);
                    }
                    else
                    {
                        Globals.QldAuth = string.Empty;
                        WriteLog("QLD Login Error: " + response.Message);
                    }
                }
                else
                {
                    Globals.QldAuth = string.Empty;
                    Console.WriteLine("Tatts Logon Failed (Unknown Reason)");
                    WriteLog("QLD Login Error: " + response.Message);
                }
            }
            #endregion Login QLD

            #region Login Default
            wsTabAuth1.apiMeta dmeta = new wsTabAuth1.apiMeta();
            dmeta.deviceId = Globals.DeviceID;
            dmeta.requestChannel = Globals.VicChan;
            dmeta.jurisdictionId = Globals.VicJury;
            dmeta.deviceIdSpecified = true;
            wsTabAuth1.thirdPartyCustomerAuthenticateRequest drequest = new wsTabAuth1.thirdPartyCustomerAuthenticateRequest();
            drequest.accountId = 1155780;
            drequest.accountPassword = "";
            wsTabAuth1.thirdPartyAuthenticate dauth = new wsTabAuth1.thirdPartyAuthenticate();
            wsTabAuth1.thirdPartyCustomerAuthenticateResponse dresp = dauth.authenticateAccount(dmeta, drequest);
            Globals.DefaultAcc = 1155780;
            Globals.DefaultPwd = "";
            Globals.DefaultAuth = dresp.usernamePasswordToken;
            #endregion

            #region Login Vic
            if (Globals.VicEnable)
            {
                wsTabAuth1.apiMeta meta = new wsTabAuth1.apiMeta();
                meta.deviceId = Globals.DeviceID;
                meta.requestChannel = Globals.VicChan;
                meta.jurisdictionId = Globals.VicJury;
                meta.deviceIdSpecified = true;
                wsTabAuth1.thirdPartyCustomerAuthenticateRequest vrequest = new wsTabAuth1.thirdPartyCustomerAuthenticateRequest();
                vrequest.accountId = Globals.VicAcc;
                vrequest.accountPassword = Globals.VicPwd;
                wsTabAuth1.thirdPartyAuthenticate auth = new wsTabAuth1.thirdPartyAuthenticate();
                wsTabAuth1.thirdPartyCustomerAuthenticateResponse resp = auth.authenticateAccount(meta, vrequest);
                Globals.VicAuth = resp.usernamePasswordToken;
            }
            #endregion

            #region Login NSW
            if (Globals.NswEnable)
            {
                wsTabAuth.apiMeta nmeta = new wsTabAuth.apiMeta();
                nmeta.deviceId = Globals.DeviceID;
                nmeta.requestChannel = Globals.NswChan;
                nmeta.jurisdictionId = Globals.NswJury;
                nmeta.deviceIdSpecified = true;
                thirdPartyCustomerAuthenticateRequest nrequest = new thirdPartyCustomerAuthenticateRequest();
                nrequest.accountId = Globals.NswAcc;
                nrequest.accountPassword = Globals.NswPwd;
                ThirdPartyCustomerAuthenticateServiceClient nauth = new ThirdPartyCustomerAuthenticateServiceClient();
                thirdPartyCustomerAuthenticateResponse nresp = nauth.authenticateAccount(nmeta, nrequest);
                Globals.NswAuth = nresp.usernamePasswordToken;
            }
            #endregion Login NSW
        }

        #region Get Balances
        public void GetBalances()
        {
            GetQldBal();
            GetVicBal();
            GetNswBal();
            Globals.TotalBal = Globals.QldBal + Globals.VicBal + Globals.NswBal;
        }

        public void GetQldBal()
        {
            string status = string.Empty;
            if (Globals.QldEnable)
            {
                TattsBalanceRequest balReq = new TattsBalanceRequest();
                balReq.AuthenticationToken = Globals.QldAuth;

                string requestJSON = JsonConvert.SerializeObject(balReq);
                string responseJSON = TattsUtils.CallAPI("account/balance/", requestJSON, true, "POST");

                TattsBalanceResponse response = null;
                if (!string.IsNullOrEmpty(responseJSON))
                {
                    response = JsonConvert.DeserializeObject<TattsBalanceResponse>(responseJSON);
                }

                if (response != null)
                {
                    status = string.Format("Current Balance: {0:C}", response.Balance);
                    Globals.QldBal = response.Balance;
                }
            }
        }

        public void GetVicBal()
        {
            if (Globals.VicEnable)
            {
                wsTabAccount.apiMeta metav = new wsTabAccount.apiMeta();
                metav.deviceId = Globals.DeviceID;
                metav.requestChannel = Globals.VicChan;
                metav.jurisdictionId = Globals.VicJury;
                metav.deviceIdSpecified = true;
                metav.usernamePasswordToken = Globals.VicAuth;

                wsTabAccount.AccountMgmtServiceClient clientv = new wsTabAccount.AccountMgmtServiceClient();
                wsTabAccount.accountBalanceRequest balreqv = new wsTabAccount.accountBalanceRequest();
                wsTabAccount.accountBalanceResponse balrespv = clientv.getAccountBalance(metav, balreqv);
                Globals.VicBal = Convert.ToDecimal(balrespv.accountBalance);
            }
        }

        public void GetNswBal()
        {
            if (Globals.NswEnable)
            {
                wsTabAccount.apiMeta metav = new wsTabAccount.apiMeta();
                metav.deviceId = Globals.DeviceID;
                metav.requestChannel = Globals.NswChan;
                metav.jurisdictionId = Globals.NswJury;
                metav.deviceIdSpecified = true;
                metav.usernamePasswordToken = Globals.NswAuth;

                wsTabAccount.AccountMgmtServiceClient clientv = new wsTabAccount.AccountMgmtServiceClient();
                wsTabAccount.accountBalanceRequest balreqv = new wsTabAccount.accountBalanceRequest();
                wsTabAccount.accountBalanceResponse balrespv = clientv.getAccountBalance(metav, balreqv);
                Globals.NswBal = Convert.ToDecimal(balrespv.accountBalance);
            }
        }
        #endregion Get Balances 

        public void LoadDefaults()
        {
            if (!CheckDatabaseExists(global.dbCon,"TheMinx"))
            {
                WriteLog("Creating Database");
                CreateDatabase();
            }
            
            global.dbCon = new SqlConnection(Globals.dbConString);
            global.dbCon.Open();

            WriteLog("Con String: " + Globals.dbConString);

            string cmd = "select * from Settings";
            SqlCommand upd = new SqlCommand(cmd, global.dbCon);
            SqlDataReader reader = null;
            try
            {
                reader = upd.ExecuteReader();
            }
            catch (Exception ex)
            {
                WriteLog("Error: " + ex.Message.ToString());
            }
            
            while (reader.Read())
            {
                Globals.VicAcc = Convert.ToInt32(reader["VicAcc"].ToString());
                Globals.VicPwd = reader["VicPwd"].ToString();
                Globals.NswAcc = Convert.ToInt32(reader["NswAcc"].ToString());
                Globals.NswPwd = reader["NswPwd"].ToString();
                Globals.QldAcc = Convert.ToInt32(reader["QldAcc"].ToString());
                Globals.QldPwd = reader["QldPwd"].ToString();
                if (reader["VicEnable"].ToString() == "true") Globals.VicEnable = true;
                if (reader["NswEnable"].ToString() == "true") Globals.NswEnable = true;
                if (reader["QldEnable"].ToString() == "true") Globals.QldEnable = true;
                Globals.StopWin = Convert.ToInt32(reader["StopWin"].ToString());
                Globals.StopLoss = Convert.ToInt32(reader["StopLoss"].ToString());
                Globals.BetToWin = Convert.ToDouble(reader["BetToWin"].ToString());
                string pct = reader["PctOn"].ToString();
                if (pct == "Y")
                {
                    Globals.PctOn = true;
                }
                else
                {
                    Globals.PctOn = false;
                }
                Globals.BetPct = Convert.ToInt32(reader["BetPct"].ToString());

                if (reader["UseRatingBetAmt"].ToString() == "Y")
                {
                    Globals.UseRatingBetAmt = true;
                }
                else
                {
                    Globals.UseRatingBetAmt = false;
                }
            }
            global.dbCon.Close();
        }

        public string GetTabRaceID(string Venue, int? RaceNum)
        {
            string raceID = "";
            RacingServiceClient raceServ = new RacingServiceClient();
            wsTabRacing.apiMeta rMeta = new wsTabRacing.apiMeta();
            rMeta.usernamePasswordToken = Globals.DefaultAuth;
            rMeta.requestChannel = Globals.VicChan;
            rMeta.jurisdictionId = Globals.VicJury;
            rMeta.deviceId = Globals.DeviceID;
            rMeta.deviceIdSpecified = true;

            raceDetailsRequestV2 detReq = new raceDetailsRequestV2();
            raceDetailsResponseV2 detResp = new raceDetailsResponseV2();
            detReq.raceNumber = Convert.ToInt32(RaceNum);
            detReq.racingCode = "G";
            if (Venue == "SANDOWN") { Venue = "SANDOWN PARK"; }
            if (Venue == "MT GAMBIER") { Venue = "MOUNT GAMBIER"; }
            if (Venue.Contains("ELWICK")) { Venue = "HOBART"; }

            detReq.meetingName = Venue;
            detReq.meetingDate = DateTime.Now.ToString("dd-MMM-yyyy");
            detResp = raceServ.getRaceDetailsV2(rMeta, detReq);

            string meetingCode = detResp.meeting.meetingIdentifier + "." +  detResp.meeting.meetingCode;
            raceID = meetingCode.ToString(); 
            return raceID;
        }

        public void UpdateNextToGo()
        {
            UpdateNTG(null, null);
            ntgTimer = new System.Timers.Timer(30000);
            ntgTimer.AutoReset = true;
            ntgTimer.Elapsed += new ElapsedEventHandler(UpdateNTG);
            ntgTimer.Start();
        }

        private void UpdateNTG(object sender, EventArgs e)
        {
            RacingServiceClient raceServ = new RacingServiceClient();
            wsTabRacing.apiMeta rMeta = new wsTabRacing.apiMeta();
            rMeta.usernamePasswordToken = Globals.DefaultAuth;
            rMeta.requestChannel = Globals.VicChan;
            rMeta.jurisdictionId = Globals.VicJury;
            rMeta.deviceId = Globals.DeviceID;
            rMeta.deviceIdSpecified = true;
            nextToGoRacesRequest ntg = new nextToGoRacesRequest();
            ntg.noOfReturnRaces = 10;
            ntg.racingCode = "G";

            string today = DateTime.Now.ToString("yyyyMMdd");
            string today2 = DateTime.Now.ToString("dd-MMM-yyyy");
            TimeSpan tme = DateTime.Now.TimeOfDay;
            if (tme.Ticks < 26000)
            {
                today = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                today2 = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            }

            nextToGoRacesResponse ntgResp = new nextToGoRacesResponse();
            try
            {
                ntgResp = raceServ.getNextToGoRaces(rMeta, ntg);

                if (ntgResp.nextToGoRaces != null)
                {
                    nextToGoRace[] ntgRaces = ntgResp.nextToGoRaces;
                    string RaceStart = ntgRaces[0].raceStartTime.ToString("hh:mm");
                    ntgRacestxt.BeginInvoke((MethodInvoker)(delegate { ntgRacestxt.Text = ""; }));
                    foreach (nextToGoRace ntgRace in ntgRaces)
                    {
                        ntgRacestxt.BeginInvoke((MethodInvoker)(delegate {
                            ntgRacestxt.AppendText("Race : " + ntgRace.raceNumber + " - " + ntgRace.meetingName + " - " + ntgRace.raceStartTime.ToString("hh:mm") + " - " + ntgRace.raceStatus + "\n");
                        }));
                    }
                    ntgLabel.BeginInvoke((MethodInvoker)(delegate { ntgLabel.Text = "Next To Go: " + ntgRaces[0].raceNumber + " - " + ntgRaces[0].meetingName + " - " + RaceStart + " - " + ntgRaces[0].raceStatus; }));
                    Globals.ntgVenue = ntgRaces[0].meetingName;
                    Globals.ntgRace = ntgRaces[0].raceNumber;
                    string raceID = ntgRaces[0].raceIdentifier;
                    string qldID = "";
                    global.dbCon = new SqlConnection(Globals.dbConString);
                    global.dbCon.Open();
                    string cmd = "select QldID from LivePrices where Venue = '" + ntgRaces[0].meetingName + "' and RaceDate = '" + today + "' and RaceNum = '" + ntgRaces[0].raceNumber + "' and RaceStatus is null";
                    SqlCommand upd = new SqlCommand(cmd, global.dbCon);
                    SqlDataReader reader = upd.ExecuteReader();
                    raceID = "";
                    while (reader.Read())
                    {
                        raceID = reader["QldID"].ToString();
                    }
                    global.dbCon.Close();

                    if (Globals.RaceThread.ContainsKey(raceID))
                    {
                    }
                    else
                    {
                        if (Globals.TodayLoaded && (raceID != ""))
                        {
                            ProcessRace race = new ProcessRace();
                            race.raceID = raceID;
                            race.venue = ntgRaces[0].meetingName;
                            race.raceNum = Convert.ToInt32(ntgRaces[0].raceNumber);
                            race.raceTime = ntgRaces[0].raceStartTime;
                            race.txtStatus = txtStatus;
                            race.qldID = raceID;

                            race.pctOn = Globals.PctOn;
                            race.useRatingBetAmount = useRatingBetAmount;
                            race.betOnVic = betOnVic;
                            race.betOnNsw = betOnNsw;
                            race.betOnQld = betOnQld;
                            race.betPct = Globals.BetPct;
                            race.betToWin = betToWin;
                            race.betOnAll = betOnAll;

                            Thread th = new Thread(new ThreadStart(race.ProcessRaceNow));
                            th.IsBackground = true;
                            Console.WriteLine("Starting Thread:" + raceID);
                            th.Start();
                            try
                            {
                                Globals.RaceThread.TryAdd(raceID, th);
                                race.thisThread = th;
                            }
                            catch { }
                        }
                    }
                }
                else
                {
                    ntgLabel.BeginInvoke((MethodInvoker)(delegate { ntgLabel.Text = "Next To Go: Error Updating Next To Go"; }));
                }
            }
            catch (Exception ex)
            {
                WriteLog("No endpoint listening");
            }
        }

        public void DisplayRaceDetail(string Venue, DateTime MeetingDate, string RaceNum, string RaceID)
        {
            upd = new PriceUpdates();
            upd.MeetingDate = MeetingDate;
            upd.Venue = Venue;
            upd.RaceNum = RaceNum;
            upd.dgRace = dgRace;
            upd.RaceID = RaceID;

            Thread th = new Thread(new ThreadStart(upd.UpdatePrices));
            th.IsBackground = true;
            th.Name = RaceID;
            if (!Globals.CurrentRace.ContainsKey(RaceID))
            {
                th.Start();
                Globals.CurrentRace.TryAdd(RaceID, th);
            }
            else
            {
                th = null;
            }
        }

        public void AutoBet(bool pctOn, int raceNum, string raceID, bool useRatingBetAmount, bool betOnVic, bool betOnNsw, bool betOnQld, double betPct, double betToWin, bool betOnAll)
        {
            ProcessAutoBet autoBet = new ProcessAutoBet();
            autoBet.pctOn = pctOn;
            autoBet.raceNum = raceNum;
            autoBet.useRatingBetAmount = useRatingBetAmount;
            autoBet.betOnVic = betOnVic;
            autoBet.betOnNsw = betOnNsw;
            autoBet.betOnQld = betOnQld;
            autoBet.betOnAll = betOnAll;
            autoBet.betPct = betPct;
            autoBet.betToWin = betToWin;

            Thread th = new Thread(new ThreadStart(autoBet.ProcessAutoBetNow));
            th.IsBackground = true;
            th.Name = "AutoBet_" + raceID;
            if (!Globals.CurrentRace.ContainsKey(th.Name))
            {
                Globals.CurrentRace.TryAdd(th.Name, th);
                th.Start();
            }
            else
            {
                th = null;
            }
        }

        public void GetPrices(object sender, ElapsedEventArgs e)
        {
            upd.UpdatePrices();
        }

        public void CreateDatabase()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\The Minx";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            SqlConnection newdb = new SqlConnection(Globals.dbConString.Replace("Initial Catalog=TheMinx", "Initial Catalog=master"));

            string str = "create database TheMinx";

            SqlCommand cmd = new SqlCommand(str, newdb);
            try
            {
                newdb.Open();
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                WriteLog("DB Error: " + ex.Message.ToString());
                Console.WriteLine("DB Error: " + ex.Message.ToString());
            }
            finally
            {
                if (newdb.State == ConnectionState.Open)
                {
                    newdb.Close();
                }
            }

            Thread.Sleep(5000);

            
            newdb = new SqlConnection(Globals.dbConString.Replace("Initial Catalog=master", "Initial Catalog=TheMinx"));
            newdb.Open();
            str = @"CREATE TABLE LivePrices (
                RaceID varchar(100),
                MeetingCode varchar(100),
                QLDMeetingCode varchar(2),
                RaceDate varchar(8),
                RaceTime varchar(8),
                Box int,
                RunnerNumber varchar(2),
                Runner varchar(100),
                QldPrice FLOAT,
                VicPrice FLOAT,
                NswPrice FLOAT,
                RaceStatus VARCHAR(50),
                RaceNum  INTEGER,
                Venue  VARCHAR(100),
                VicID  VARCHAR(100),
                NswID  VARCHAR(100),
                QldID  VARCHAR(100),
                Tip  VARCHAR(2),
                MyPrice  FLOAT,
                BetAmt  FLOAT,
                VicPlace  FLOAT,
                NswPlace  FLOAT,
                QldPlace  FLOAT,
                BestWin  FLOAT,
                BestPlace  FLOAT,
                BestPct FLOAT,
                Status  VARCHAR(20),
                Resulted INTEGER,
                BetStatus VARCHAR(100),
                Updated VARCHAR(1),
                PayQLD VARCHAR(8),
                PayNSW VARCHAR(8),
                PayVIC VARCHAR(8),
                PayQLDPlace VARCHAR(8),
                PayNSWPlace VARCHAR(8),
                PayVICPlace VARCHAR(8),
                BetAmount VARCHAR(8),
                BetState VARCHAR(3))";

            cmd = new SqlCommand(str, newdb);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                WriteLog("DB Error: " + ex.Message.ToString());
            }

            str = @"CREATE TABLE Bets (
                RaceID varchar(20) NOT NULL,
	            RaceDate varchar(8) NOT NULL,
	            Venue varchar(100) NULL,
	            RaceNum int NULL,
	            Box int NULL,
	            Runner varchar(100) NULL,
	            QLDPrice float NULL,
	            NSWPrice float NULL,
	            VICPrice float NULL,
	            QLDPlace float NULL,
	            NSWPlace float NULL,
	            VICPlace float NULL,
	            Tip varchar(2) NULL,
	            BestWin float NULL,
	            BestPlace float NULL,
	            BestPct float NULL,
	            BetStatus varchar(100) NULL,
	            MyPrice float NULL,
	            MyBetAmt float NULL,
	            BetAmt float NULL,
	            BetState varchar(3) NULL,
	            PayQLD float NULL,
	            PayNSW float NULL,
	            PayVIC float NULL,
	            PayQLDPlace float NULL,
	            PayNSWPlace float NULL,
	            PayVICPlace float NULL,
                LiveBetting varchar(5),
                Position int NULL)";
                cmd = new SqlCommand(str, newdb);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                WriteLog("DB Error: " + ex.Message.ToString());
            }


            str = @"CREATE TABLE [TrackAbbrev] ([TrackID] VARCHAR(3) PRIMARY KEY,[TrackName] VARCHAR(100) NULL)";
            cmd = new SqlCommand(str,newdb);
            cmd.ExecuteNonQuery();


            str = @"INSERT INTO TrackAbbrev VALUES ('ACT', 'CANBERRA');
                    INSERT INTO TrackAbbrev VALUES ('ADD', 'ADDINGTON');
                    INSERT INTO TrackAbbrev VALUES ('APK', 'ANGLE PARK');
                    INSERT INTO TrackAbbrev VALUES ('ASC', 'ASCOT PARK');
                    INSERT INTO TrackAbbrev VALUES ('BAL', 'BALLARAT');
                    INSERT INTO TrackAbbrev VALUES ('BAR', 'BARMERA');
                    INSERT INTO TrackAbbrev VALUES ('BAT', 'BATHURST');
                    INSERT INTO TrackAbbrev VALUES ('BEN', 'BENDIGO');
                    INSERT INTO TrackAbbrev VALUES ('BGC', 'ALBION PARK');
                    INSERT INTO TrackAbbrev VALUES ('BUL', 'BULLI');
                    INSERT INTO TrackAbbrev VALUES ('CAI', 'CAIRNS');
                    INSERT INTO TrackAbbrev VALUES ('CAM', 'CAMBRIDGE');
                    INSERT INTO TrackAbbrev VALUES ('CAN', 'CANNINGTON');
                    INSERT INTO TrackAbbrev VALUES ('CAS', 'CASINO');
                    INSERT INTO TrackAbbrev VALUES ('CES', 'CESSNOCK');
                    INSERT INTO TrackAbbrev VALUES ('COO', 'COONAMBLE');
                    INSERT INTO TrackAbbrev VALUES ('CRN', 'CRANBOURNE');
                    INSERT INTO TrackAbbrev VALUES ('DAR', 'DARWIN');
                    INSERT INTO TrackAbbrev VALUES ('DEV', 'DEVONPORT');
                    INSERT INTO TrackAbbrev VALUES ('DTO', 'DAPTO');
                    INSERT INTO TrackAbbrev VALUES ('DUB', 'DUBBO');
                    INSERT INTO TrackAbbrev VALUES ('ELW', 'ELWICK (HOB)');
                    INSERT INTO TrackAbbrev VALUES ('FBY', 'FORBURY PARK');
                    INSERT INTO TrackAbbrev VALUES ('GAR', 'THE GARDENS');
                    INSERT INTO TrackAbbrev VALUES ('GAW', 'GAWLER');
                    INSERT INTO TrackAbbrev VALUES ('GBN', 'GOULBURN');
                    INSERT INTO TrackAbbrev VALUES ('GEL', 'GEELONG');
                    INSERT INTO TrackAbbrev VALUES ('GOS', 'GOSFORD');
                    INSERT INTO TrackAbbrev VALUES ('GRA', 'GRAFTON');
                    INSERT INTO TrackAbbrev VALUES ('HAT', 'HATRICK');
                    INSERT INTO TrackAbbrev VALUES ('HEA', 'HEALESVILLE');
                    INSERT INTO TrackAbbrev VALUES ('HOR', 'HORSHAM');
                    INSERT INTO TrackAbbrev VALUES ('IPS', 'IPSWICH');
                    INSERT INTO TrackAbbrev VALUES ('LCN', 'LAUNCESTON');
                    INSERT INTO TrackAbbrev VALUES ('LIS', 'LISMORE');
                    INSERT INTO TrackAbbrev VALUES ('MAN', 'MANDURAH');
                    INSERT INTO TrackAbbrev VALUES ('MAT', 'MAITLAND');
                    INSERT INTO TrackAbbrev VALUES ('MEA', 'THE MEADOWS');
                    INSERT INTO TrackAbbrev VALUES ('MKA', 'MANUKAU');
                    INSERT INTO TrackAbbrev VALUES ('MNA', 'MANAWATU');
                    INSERT INTO TrackAbbrev VALUES ('MTG', 'MT GAMBIER');
                    INSERT INTO TrackAbbrev VALUES ('NOR', 'NORTHAM');
                    INSERT INTO TrackAbbrev VALUES ('NOW', 'NOWRA');
                    INSERT INTO TrackAbbrev VALUES ('POR', 'PORT PIRIE');
                    INSERT INTO TrackAbbrev VALUES ('POT', 'POTTS PARK');
                    INSERT INTO TrackAbbrev VALUES ('PTA', 'PORT AUGUSTA');
                    INSERT INTO TrackAbbrev VALUES ('RIC', 'RICHMOND');
                    INSERT INTO TrackAbbrev VALUES ('ROC', 'ROCKHAMPTON');
                    INSERT INTO TrackAbbrev VALUES ('SAN', 'SANDOWN');
                    INSERT INTO TrackAbbrev VALUES ('SHP', 'SHEPPARTON');
                    INSERT INTO TrackAbbrev VALUES ('SLE', 'SALE');
                    INSERT INTO TrackAbbrev VALUES ('STR', 'STRATHALBYN');
                    INSERT INTO TrackAbbrev VALUES ('TAK', 'TARANAKI');
                    INSERT INTO TrackAbbrev VALUES ('TAM', 'TAMWORTH');
                    INSERT INTO TrackAbbrev VALUES ('TEM', 'TEMORA');
                    INSERT INTO TrackAbbrev VALUES ('TRA', 'TRARALGON');
                    INSERT INTO TrackAbbrev VALUES ('TVL', 'TOWNSVILLE');
                    INSERT INTO TrackAbbrev VALUES ('WAG', 'WAGGA');
                    INSERT INTO TrackAbbrev VALUES ('WAR', 'WARRNAMBOOL');
                    INSERT INTO TrackAbbrev VALUES ('WGL', 'WARRAGUL');
                    INSERT INTO TrackAbbrev VALUES ('WPK', 'WENTWORTH PARK');
                    INSERT INTO TrackAbbrev VALUES ('WTA', 'WANGARATTA');
                    ";
            cmd = new SqlCommand(str,newdb);
            cmd.ExecuteNonQuery();

            str = @"CREATE TABLE Settings (
                    VicAcc  INTEGER,
                    VicPwd  VARCHAR(50),
                    NswAcc  INTEGER,
                    NswPwd  VARCHAR(50),
                    QldAcc  INTEGER,
                    QldPwd  VARCHAR(50),
                    VicEnable  VARCHAR(5),
                    NswEnable  VARCHAR(5),
                    QldEnable  VARCHAR(5),
                    DBPath  VARCHAR(500),
                    StopWin  INTEGER,
                    StopLoss  INTEGER,
                    BetToWin FLOAT,
                    PctOn VARCHAR(1),
                    BetPct INTEGER,
                    UseRatingBetAmt VARCHAR(1))";

            cmd = new SqlCommand(str,newdb);
            cmd.ExecuteNonQuery();

            str = "INSERT INTO Settings (VicAcc, VicPwd, NswAcc, NswPwd, QldAcc, QldPwd, StopWin, StopLoss, BetToWin, PctOn, BetPct) values ('0','0','0','0','0','0','0','0','0','Y','0')";
            cmd = new SqlCommand(str, newdb);
            cmd.ExecuteNonQuery();

            str = @"CREATE VIEW TodaysVenue AS 
                    SELECT DISTINCT
                    LivePrices.Venue
                    FROM
                    LivePrices
                    WHERE
                    LivePrices.RaceDate = convert(varchar, getdate(), 112)";

            cmd = new SqlCommand(str,newdb);
            cmd.ExecuteNonQuery();

            str = @"CREATE VIEW Results_Report AS 
                    SELECT RaceDate, RaceTime, Box, Runner, RaceNum, Venue, BestWin, BestPlace, BestPct, Resulted, BetStatus, PayQLD, PayNSW, PayVIC, PayQLDPlace, PayVICPlace, PayNSWPlace, BetAmount, BetState 
                    FROM   LivePrices";

            cmd = new SqlCommand(str, newdb);
            cmd.ExecuteNonQuery();

            newdb.Close();
 
        }

        public void WriteLog(string error)
        {
            try
            {
                if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\The Minx\Log.txt"))
                {
                    using (FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\The Minx\Log.txt", FileMode.Append))
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine(error);
                        sw.Close();
                    }
                }
                else
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\The Minx";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\The Minx\Log.txt", FileMode.OpenOrCreate))
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine(error);
                        sw.Close();
                    }
                }
            }
            catch { }

        }

        public void UpdateResults()
        {
            RacingServiceClient raceServ = new RacingServiceClient();
            wsTabRacing.apiMeta vMeta = new wsTabRacing.apiMeta();
            vMeta.usernamePasswordToken = Globals.DefaultAuth;
            vMeta.requestChannel = Globals.VicChan;
            vMeta.jurisdictionId = Globals.VicJury;
            vMeta.deviceId = Globals.DeviceID;
            vMeta.deviceIdSpecified = true;

            wsTabRacing.apiMeta nMeta = new wsTabRacing.apiMeta();
            nMeta.usernamePasswordToken = Globals.DefaultAuth;
            nMeta.requestChannel = Globals.NswChan;
            nMeta.jurisdictionId = Globals.NswJury;
            nMeta.deviceId = Globals.DeviceID;
            nMeta.deviceIdSpecified = true;

            string today = DateTime.Now.ToString("yyyyMMdd");
            string today2 = DateTime.Now.ToString("dd-MMM-yyyy");
            bool yesterday = false;
            double tme = DateTime.Now.TimeOfDay.TotalHours;
            if (tme > 0 && tme < 8)
            {
                today = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                today2 = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                yesterday = true;
            }

#region VIC Results
            using (SqlConnection con = new SqlConnection(Globals.dbConString))
            {
                string cmd = "";
                con.Open();
                if (!yesterday)
                {
                    cmd = "select distinct Venue,RaceNum,RaceID,RaceDate from LivePrices where Updated is null and RaceDate = '" + today + "' and RaceTime < '" + DateTime.Now.ToString("HH:mm:00") + "'";
                }
                else
                {
                    cmd = "select distinct Venue,RaceNum,RaceID,RaceDate from LivePrices where RaceDate = '" + today + "'";
                }

                SqlCommand upd = new SqlCommand(cmd, con);
                SqlDataReader reader = null;
                try
                {
                    reader = upd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    WriteLog("Error: " + ex.Message.ToString());
                }

                while (reader.Read())
                {
                    string venue = reader["Venue"].ToString();
                    string racenum = reader["RaceNum"].ToString();
                    resultedRaceDetailsRequest req = new resultedRaceDetailsRequest();
                    resultedRaceDetailsResponse resp = new resultedRaceDetailsResponse();
                    req.racingCode = "G";
                    req.meetingDate = today2;
                    req.meetingName = venue;
                    req.raceNumber = racenum;

                    resp = raceServ.getResultedRaceDetails(vMeta, req);
                    if (resp.raceStatus == "ALL_POOL_RESULTED")
                    {
                        SqlConnection resdb = new SqlConnection(Globals.dbConString);
                        resdb.Open();
                        foreach (racingResultDetail detail in resp.racingResultDetailList)
                        {
                            int box = Convert.ToInt32(detail.runnerNo);
                            int? pos = Convert.ToInt32(detail.finishPosition);
                            string rslt = "";
                            string bet = "";
                            if (pos > 0)
                            {
                                if (pos == 1)
                                {
                                    rslt = "update LivePrices set Resulted = " + pos + ", PayVIC = '" + detail.toteWinOdds + "', PayVICPlace = '" + detail.totePlaceOdds + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                    bet = "update Bets set Position = " + pos + ",  PayVIC = '" + detail.toteWinOdds + "', PayVICPlace = '" + detail.totePlaceOdds + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                }
                                else
                                {
                                    rslt = "update LivePrices set Resulted = " + pos + ", PayVICPlace = '" + detail.totePlaceOdds + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                    bet = "update Bets set Position = " + pos + ", PayVICPlace = '" + detail.totePlaceOdds + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                }
                                SqlCommand cmdupd = new SqlCommand(rslt, resdb);
                                try
                                {
                                    cmdupd.ExecuteNonQuery();
                                }
                                catch (Exception e1)
                                {
                                    WriteLog("Error: " + e1.Message.ToString());
                                }
                                
                                SqlCommand cmdbet = new SqlCommand(bet, resdb);
                                try
                                {
                                    cmdbet.ExecuteNonQuery();
                                }
                                catch (Exception e1)
                                {
                                    WriteLog("Error: " + e1.Message.ToString());
                                }

                            }
                            else
                            {
//                                string rslt = "update LivePrices set Updated = 'Y' where RaceDate = '" + DateTime.Now.ToString("yyyyMMdd") + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
//                                SqlCommand cmdupd = new SqlCommand(rslt, resdb);
//                                try
//                                {
//                                    cmdupd.ExecuteNonQuery();
//                                }
//                                catch (Exception e1)
//                                {
//                                    WriteLog("Error: " + e1.Message.ToString());
//                                }
// 
                            }
                        }
                        resdb.Close();
                    }
                }
                reader.Close();
            }
# endregion

#region NSW Results
            using (SqlConnection con = new SqlConnection(Globals.dbConString))
            {
                string cmd = "";
                con.Open();
                if (!yesterday)
                {
                    cmd = "select distinct Venue,RaceNum,RaceID,RaceDate from LivePrices where Updated is null and RaceDate = '" + today + "' and RaceTime < '" + DateTime.Now.ToString("HH:mm:00") + "'";
                }
                else
                {
                    cmd = "select distinct Venue,RaceNum,RaceID,RaceDate from LivePrices where RaceDate = '" + today + "'";
                }

                SqlCommand upd = new SqlCommand(cmd, con);
                SqlDataReader reader = null;
                try
                {
                    reader = upd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    WriteLog("Error: " + ex.Message.ToString());
                }

                while (reader.Read())
                {
                    string venue = reader["Venue"].ToString();
                    string racenum = reader["RaceNum"].ToString();
                    resultedRaceDetailsRequest req = new resultedRaceDetailsRequest();
                    resultedRaceDetailsResponse resp = new resultedRaceDetailsResponse();
                    req.racingCode = "G";
                    req.meetingDate = today2;
                    req.meetingName = venue;
                    req.raceNumber = racenum;

                    resp = raceServ.getResultedRaceDetails(nMeta, req);
                    if (resp.raceStatus == "ALL_POOL_RESULTED")
                    {
                        SqlConnection resdb = new SqlConnection(Globals.dbConString);
                        resdb.Open();
                        foreach (racingResultDetail detail in resp.racingResultDetailList)
                        {
                            int box = Convert.ToInt32(detail.runnerNo);
                            int? pos = Convert.ToInt32(detail.finishPosition);
                            string rslt = "";
                            string bet = "";
                            if (pos > 0)
                            {
                                if (pos == 1)
                                {
                                    rslt = "update LivePrices set PayNSW = '" + detail.toteWinOdds + "', PayNSWPlace = '" + detail.totePlaceOdds + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                    bet = "update Bets set Position = " + pos + ",  PayNSW = '" + detail.toteWinOdds + "', PayNSWPlace = '" + detail.totePlaceOdds + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                }
                                else
                                {
                                    rslt = "update LivePrices set Resulted = " + pos + ", PayNSWPlace = '" + detail.totePlaceOdds + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                    bet = "update Bets set Position = " + pos + ", PayNSWPlace = '" + detail.totePlaceOdds + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;

                                }

                                SqlCommand cmdupd = new SqlCommand(rslt, resdb);
                                try
                                {
                                    cmdupd.ExecuteNonQuery();
                                }
                                catch (Exception e1)
                                {
                                    WriteLog("Error: " + e1.Message.ToString());
                                }

                                SqlCommand cmdbet = new SqlCommand(bet, resdb);
                                try
                                {
                                    cmdbet.ExecuteNonQuery();
                                }
                                catch (Exception e1)
                                {
                                    WriteLog("Error: " + e1.Message.ToString());
                                }
                            }
                        }
                        resdb.Close();
                    }
                }
            }
#endregion                

#region QLD Resutls

            string dt = DateTime.Now.ToString("yyyyMMdd");
            string first = ""; string second = ""; string third = ""; string win = "";
                
            string dt1 = dt;
            string timeNow = "";
            double mins = DateTime.Now.TimeOfDay.TotalMinutes;
            if (mins < 690)
            {
                timeNow = DateTime.Now.AddMinutes(30).ToString("HH:mm");
            }
            else
            {
                timeNow = DateTime.Now.ToString("HH:mm");
            }

            double secs = DateTime.Now.TimeOfDay.TotalSeconds;
            if (secs <= 21600)
            {
                dt = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            }

            if (yesterday) dt = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            using (SqlConnection con = new SqlConnection(Globals.dbConString))
            {
                con.Open();
                SqlDataReader qreader = null;
                string cmd = "";
                if (!yesterday)
                {
                    cmd = "select distinct Venue,RaceNum,RaceID,RaceDate,QLDMeetingCode from LivePrices where Updated is null and RaceDate = '" + today + "' and RaceTime < '" + DateTime.Now.ToString("HH:mm:00") + "'";
                }
                else
                {
                    cmd = "select distinct Venue,RaceNum,RaceID,RaceDate,QLDMeetingCode from LivePrices where RaceDate = '" + today + "'";
                }

//                cmd = "select distinct Venue,RaceNum,RaceID,RaceDate,QLDMeetingCode from LivePrices where RaceID = 'G.SAN.20141212.1' and Box = '7'";

                SqlCommand upd = new SqlCommand(cmd, con);
                try
                {
                    qreader = upd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    WriteLog("Error: " + ex.Message.ToString());
                }

                while (qreader.Read())
                {
                    string RaceID = qreader["RaceID"].ToString();
                    string racedate = qreader["RaceDate"].ToString();
                    string venue = qreader["Venue"].ToString();
                    string racenum = qreader["RaceNum"].ToString();
                    string qldcode = qreader["QLDMeetingCode"].ToString();

                    if (RaceID != null)
                    {
                        string dogQry;
                        int pos = 0;
                        string yr = racedate.Substring(0, 4);
                        string mth = racedate.Substring(4, 2);
                        string day = racedate.Substring(6, 2);

                        dt1 = yr + "/" + ((Convert.ToInt32(mth)).ToString()) + "/" + ((Convert.ToInt32(day)).ToString());
                        try
                        {
                            HttpWebRequest reqRace = (HttpWebRequest)WebRequest.Create("http://tatts.com/pagedata/racing/" + dt1 + "/" + qldcode + racenum + ".xml");
                            HttpWebResponse resRace = (HttpWebResponse)reqRace.GetResponse();
                            Stream strRace = resRace.GetResponseStream();
                            XmlDocument docRace = new XmlDocument();
                            docRace.Load(strRace);

                            foreach (XmlNode nRace in docRace.ChildNodes)
                            {
                                XmlNode results = docRace.FirstChild;
                                foreach (XmlNode node in results.ChildNodes)
                                {
                                    foreach (XmlNode mNode in node)
                                    {
                                        if (mNode.Name == "Race")
                                        {
                                            foreach (XmlNode rNode in mNode)
                                            {
                                                if (rNode.Name == "ResultPlace")
                                                {
                                                    string placeNo = rNode.Attributes["PlaceNo"].Value;
                                                    foreach (XmlNode result in rNode)
                                                    {
                                                        string box = result.Attributes["RunnerNo"].Value;
                                                        string poolType = result.Attributes["PoolType"].Value;
                                                        foreach (XmlNode pType in result)
                                                        {
                                                            SqlConnection resdb = new SqlConnection(Globals.dbConString);
                                                            resdb.Open();
                                                            if (placeNo == "1")
                                                            {
                                                                if (poolType == "WW")
                                                                {
                                                                    win = pType.Attributes["Dividend"].Value;
                                                                }
                                                                if (poolType == "PP")
                                                                {
                                                                    try
                                                                    {
                                                                        first = pType.Attributes["Dividend"].Value;
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        first = "0";
                                                                    }
                                                                }
                                                                string rslt = "update LivePrices set PayQLD = '" + win + "', PayQLDPlace = '" + first + "', Updated = 'Y' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                                                string bet = "update Bets set Position = 1, PayQLD = '" + win + "', PayQLDPlace = '" + first + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                                                SqlCommand cmdupd = new SqlCommand(rslt, resdb);
                                                                try
                                                                {
                                                                    cmdupd.ExecuteNonQuery();
                                                                }
                                                                catch (Exception e1)
                                                                {
                                                                    WriteLog("Error: " + e1.Message.ToString());
                                                                }

                                                                SqlCommand betcmd = new SqlCommand(bet, resdb);
                                                                try
                                                                {
                                                                    betcmd.ExecuteNonQuery();
                                                                }
                                                                catch (Exception e1)
                                                                {
                                                                    WriteLog("Error: " + e1.Message.ToString());
                                                                }
                                                                
                                                            }

                                                            if (placeNo == "2")
                                                            {
                                                                if (poolType == "PP")
                                                                {
                                                                    try
                                                                    {
                                                                        second = pType.Attributes["Dividend"].Value;
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        second = "0";
                                                                    }
                                                                    string rslt = "update LivePrices set PayQLDPlace = '" + second + "', Updated = 'Y' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                                                    string bet = "update Bets set Position = 2, PayQLDPlace = '" + second + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                                                    SqlCommand cmdupd = new SqlCommand(rslt, resdb);
                                                                    try
                                                                    {
                                                                        cmdupd.ExecuteNonQuery();
                                                                    }
                                                                    catch (Exception e1)
                                                                    {
                                                                        WriteLog("Error: " + e1.Message.ToString());
                                                                    }

                                                                    SqlCommand betcmd = new SqlCommand(bet, resdb);
                                                                    try
                                                                    {
                                                                        betcmd.ExecuteNonQuery();
                                                                    }
                                                                    catch (Exception e1)
                                                                    {
                                                                        WriteLog("Error: " + e1.Message.ToString());
                                                                    }
                                                                }
                                                            }

                                                            if (placeNo == "3")
                                                            {
                                                                if (poolType == "PP")
                                                                {
                                                                    try
                                                                    {
                                                                        third = pType.Attributes["Dividend"].Value;
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        third = "0";
                                                                    }
                                                                    string rslt = "update LivePrices set PayQLDPlace = '" + third + "', Updated = 'Y' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                                                    string bet = "update Bets set Position = 3, PayQLDPlace = '" + third + "' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                                                                    SqlCommand cmdupd = new SqlCommand(rslt, resdb);
                                                                    try
                                                                    {
                                                                        cmdupd.ExecuteNonQuery();
                                                                    }
                                                                    catch (Exception e1)
                                                                    {
                                                                        WriteLog("Error: " + e1.Message.ToString());
                                                                    }

                                                                    SqlCommand betcmd = new SqlCommand(bet, resdb);
                                                                    try
                                                                    {
                                                                        betcmd.ExecuteNonQuery();
                                                                    }
                                                                    catch (Exception e1)
                                                                    {
                                                                        WriteLog("Error: " + e1.Message.ToString());
                                                                    }
                                                                }
                                                            }

                                                            resdb.Close();
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
            }

            using (SqlConnection con = new SqlConnection(Globals.dbConString))
            {
                con.Open();
                SqlDataReader qreader = null;
                string cmd = "select distinct Venue,RaceNum,RaceID,RaceDate,Box,PayQLD,PayNSW,PayVIC from LivePrices where Updated is null and RaceDate = '" + today + "' and RaceTime < '" + DateTime.Now.ToString("HH:mm:00") + "'";
                SqlCommand upd = new SqlCommand(cmd, con);
                try
                {
                    qreader = upd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    WriteLog("Error: " + ex.Message.ToString());
                }

                while (qreader.Read())
                {
                    string RaceID = qreader["RaceID"].ToString();
                    string racedate = qreader["RaceDate"].ToString();
                    string venue = qreader["Venue"].ToString();
                    string racenum = qreader["RaceNum"].ToString();
                    string box = qreader["Box"].ToString();
                    string payvic = qreader["PayVIC"].ToString();
                    string paynsw = qreader["PayNSW"].ToString();
                    string payqld = qreader["PayQLD"].ToString();
                    if (payvic == "" && paynsw == "" && payqld == "")
                    {
                        SqlConnection resdb = new SqlConnection(Globals.dbConString);
                        resdb.Open();
                        string rslt = "update LivePrices set Updated = 'Y' where RaceDate = '" + today + "' and Venue = '" + venue + "' and RaceNum = " + racenum + " and Box = " + box;
                        SqlCommand cmdupd = new SqlCommand(rslt, resdb);
                        try
                        {
                            cmdupd.ExecuteNonQuery();
                        }
                        catch (Exception e1)
                        {
                            WriteLog("Error Utils line 1265: " + e1.Message.ToString());
                        }
                        resdb.Close();
                    }
                }
            }
#endregion

        }

        private static bool CheckDatabaseExists(SqlConnection tmpConn, string databaseName)
        {
            string sqlCreateDBQuery;
            bool result = false;

            try
            {
                tmpConn = new SqlConnection(Globals.dbConString);

                sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);

                using (tmpConn)
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, tmpConn))
                    {
                        tmpConn.Open();
                        int databaseID = (int)sqlCmd.ExecuteScalar();    
                        tmpConn.Close();

                        result = (databaseID > 0);
                    }
                }
            } 
            catch (Exception ex)
            { 
                result = false;
            }

            return result;
        }

        public DataSet XmlString2DataSet(string xmlString)
        {
            DataSet ds = null;
            using (StringReader sr = new StringReader(xmlString))
            {
                ds = new DataSet();
                ds.ReadXml(sr);
            }
            return ds;
        }

        public void UpdateScratchings()
        {
            if (global.dbCon.State == System.Data.ConnectionState.Closed) global.dbCon.Open();

            RacingServiceClient raceServ = new RacingServiceClient();
            wsTabRacing.apiMeta rMeta = new wsTabRacing.apiMeta();
            rMeta.usernamePasswordToken = Globals.DefaultAuth;
            rMeta.requestChannel = Globals.VicChan;
            rMeta.jurisdictionId = Globals.VicJury;
            rMeta.deviceId = Globals.DeviceID;
            rMeta.deviceIdSpecified = true;

            meetingsRequest mreq = new meetingsRequest();
            meetingsResponse mresp = new meetingsResponse();
            mreq.racingCode = "G";
            mresp = raceServ.getMeetings(rMeta, mreq);
            foreach (meeting1 meet in mresp.meeting)
            {
                meetingDetailsRequest mDetReq = new meetingDetailsRequest();
                meetingDetailsResponse mDetResp = new meetingDetailsResponse();
                mDetReq.meetingDateIdentifier = meet.meetingDateString;
                mDetReq.meetingName = meet.meetingName;
                mDetReq.racingCode = meet.racingCode;
                mDetResp = raceServ.getMeetingDetails(rMeta, mDetReq);

                foreach (raceDetail rdet in mDetResp.meeting.raceDetailList)
                {
                    string raceID = meet.meetingIdentifier + "." + rdet.raceNumber;
                    string venue = meet.meetingName;
                    int racenum = Convert.ToInt32(rdet.raceNumber);
                    string jump = rdet.raceStartTime.ToString("HH:mm:ss");
                    string raceDate = meet.meetingDate.ToString("yyyyMMdd");
                    raceDate = DateTime.Now.ToString("yyyyMMdd");

                    raceSelectionDetail[] rRunners = rdet.raceSelections;
                    foreach (raceSelectionDetail runner in rRunners)
                    {
                        int box = (int)runner.number;
                        string name = runner.runner;
                        name = name.Replace("'", "");
                        string runnerNumber = runner.barrierNumber.ToString();
                        string status = runner.status;
                        string upd = "update LivePrices set Status = '" + status + "' where RaceID = '" + raceID + "' and RaceDate = '" + raceDate + "' and Box = '" + box + "'";
                        //                        string upd = "insert into LivePrices (RaceID, RaceDate, Box, RaceTime, Runner, RunnerNumber, MeetingCode, RaceNum, Venue, QldID, VicID, NswID, Status) values ('" + raceID + "','" + raceDate + "'," + box + ",'" + jump + "','" + name + "','" + runnerNumber + "','" + meet.meetingCode + "','" + rdet.raceNumber + "','" + venue + "','" + "" + "','" + raceID + "','" + raceID + "','" + status + "')";
                        SqlCommand cmdupd = new SqlCommand(upd, global.dbCon);
                        try
                        {
                            if (global.dbCon.State == System.Data.ConnectionState.Closed) global.dbCon.Open();
                            cmdupd.ExecuteNonQuery();
                        }
                        catch (Exception e1)
                        {
                            Console.WriteLine(e1.Message.ToString());
                        }
                    }
                }
            }
//            prog.Stop();
            prog.Invoke(new MethodInvoker(delegate { prog.Visible = false; }));
            MessageBox.Show("Scratchings Updated");
        }

        public static double ToNumber(string val)
        {
            if (string.IsNullOrEmpty(val)) val = "0";
            double num = 0;
            num = Convert.ToDouble(val);
            return num;
        }

        public static double ToWinnings(string vic, string nsw, string qld, double betamt)
        {
            double win = 0;
            double dvic, dnsw, dqld;
            if (string.IsNullOrEmpty(vic)) vic = "0";
            if (string.IsNullOrEmpty(nsw)) nsw = "0";
            if (string.IsNullOrEmpty(qld)) qld = "0";
            dvic = Convert.ToDouble(vic);
            dnsw = Convert.ToDouble(nsw);
            dqld = Convert.ToDouble(qld);

            win = ((dvic + dnsw + dqld) * betamt) - betamt;
            return win;
        }
    }
}
