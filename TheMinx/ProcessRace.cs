using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;
using TheMinx.wsTabRacing;
using System.Data.SqlClient;

namespace TheMinx
{
    public class ProcessRace : IDisposable
    {
        Globals global = new Globals();
        public string raceID, venue, qldID, qldRaceCode;
        public int raceNum;
        public DateTime raceTime;
        public Thread thisThread;
        public KryptonRichTextBox txtStatus;

        private decimal[] MyPrice = new decimal[10];
        private string[] MyBet = new string[10];
        private string[] Tip = new string[10];
        private string[] MeetingCode = new string[10];
        private string[] QLDMeetingCode = new string[10];

        public bool pctOn { get; set; }
        public bool useRatingBetAmount { get; set; }
        public bool betOnVic { get; set; }
        public bool betOnNsw { get; set; }
        public bool betOnQld { get; set; }
        public bool betOnAll { get; set; }
        public double betPct { get; set; }
        public double betToWin { get; set; }

        private List<RunningRace> races = new List<RunningRace>();
        private List<string> listRunners = new List<string>();
        private double totalPct = 0;

        public ProcessRace()
        {
        }

        public void ProcessRaceNow()
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            string today2 = DateTime.Now.ToString("dd/MM/yyyy");
            string today3 = DateTime.Now.ToString("dd-MMM-yyyy");
            TimeSpan tme = DateTime.Now.TimeOfDay;
            if (tme.Ticks < 26000)
            {
                today = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                today2 = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                today3 = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            }
            bool ready = true;
            string racetime = "";
            DateTime dtRaceTime;
            SqlConnection dbCon = new SqlConnection(Globals.dbConString);
            dbCon.Open();
            string cmd = "select Box, coalesce(Tip,'1') as Tip, coalesce(MyPrice,'0') as MyPrice, coalesce(BetAmt,'0') as BetAmt, RaceNum, RaceID, MeetingCode, QLDMeetingCode, RaceTime from LivePrices where Venue = '" + venue + "' and RaceDate = '" + today + "' and RaceNum = '" + raceNum + "' and RaceStatus is null";
            SqlCommand upd = new SqlCommand(cmd, dbCon);
            SqlDataReader reader = upd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    int box = Convert.ToInt32(reader["Box"].ToString());
                    box -= 1;
                    try
                    {
                        MyPrice[box] = Convert.ToDecimal(reader["MyPrice"].ToString());
                        MyBet[box] = reader["BetAmt"].ToString();
                        Tip[box] = reader["Tip"].ToString();
                    }
                    catch
                    {
                        MyPrice[box] = 0;
                        MyBet[box] = "0";
                        Tip[box] = "1";
                    }
                    if (Tip[box] == null)
                    {
                        Console.WriteLine("Tip Error:" + Tip[box]);
                    }
                    MeetingCode[box] = reader["RaceID"].ToString();
                    QLDMeetingCode[box] = reader["QLDMeetingCode"].ToString();
                    raceID = reader["RaceID"].ToString();
                    raceNum = Convert.ToInt32(reader["RaceNum"].ToString());
                    racetime = reader["RaceTime"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Process Race Line 95: " + ex.Message.ToString());
                    ready = false;
                }
            }
            dbCon.Close();

            //            ready = true;

            dtRaceTime = Convert.ToDateTime(today2 + " " + racetime);
            dtRaceTime = dtRaceTime.AddMinutes(-1);

            txtStatus.Invoke(new MethodInvoker(delegate
            {
                txtStatus.AppendText(string.Format("Starting Processing Race {0} - {1} - {2}", venue, raceNum, raceTime));
                txtStatus.AppendText("\n");
            }));

            // Process QLD      
            while (ready)
            {
                if (DateTime.Now >= dtRaceTime)
                {
                    DataResponse response = null;
                    try
                    {
                        string responseJSON = TattsUtils.CallAPI(qldID, null, true, "GET");
                        response = JsonConvert.DeserializeObject<DataResponse>(responseJSON);
                        //                    if ((response.Data[0].Meetings[0].Races[0].Status == "CLOSED") || (response.Data[0].Meetings[0].Races[0].Status == "ABANDONED") || (response.Data[0].Meetings[0].Races[0].Status == "PAYING"))
                        //                    {
                        //                        ready = false;
                        //                    }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error Getting Race Data - QLD -  " + ex.Message.ToString());
                    }

                    try
                    {
                        if (response.Data.Count > 0)
                        {
                            foreach (TattsRunner runner in response.Data[0].Meetings[0].Races[0].Runners)
                            {
                                RunningRace race = new RunningRace();
                                race.Dog = runner.RunnerName;
                                race.Box = (int)runner.RunnerNumber;
                                race.QldPrice = (double)runner.WinOdds;
                                race.QldPlace = (double)runner.PlaceOdds;
                                race.MyPrice = (double)MyPrice[race.Box - 1];
                                race.BetAmt = Convert.ToDouble(MyBet[race.Box - 1]);
                                race.Tip = Tip[race.Box - 1];

                                if (race.QldPrice > 1000) race.QldPrice = 0;
                                if (race.QldPlace > 1000) race.QldPlace = 0;
                                race.Status = response.Data[0].Meetings[0].Races[0].Status;
                                race.RaceNum = raceNum;

                                string key = raceID + "_" + runner.RunnerNumber.ToString();

                                if (!listRunners.Contains(key))
                                {
                                    races.Add(race);
                                    listRunners.Add(key);
                                }
                            }

                            // Process VIC
                            RacingServiceClient raceServ = new RacingServiceClient();
                            wsTabRacing.apiMeta vMeta = new wsTabRacing.apiMeta();
                            vMeta.usernamePasswordToken = Globals.DefaultAuth;
                            vMeta.requestChannel = Globals.VicChan;
                            vMeta.jurisdictionId = Globals.VicJury;
                            vMeta.deviceId = Globals.DeviceID;
                            vMeta.deviceIdSpecified = true;
                            raceDetailsRequestV2 detReq = new raceDetailsRequestV2();
                            raceDetailsResponseV2 detResp = new raceDetailsResponseV2();
                            detReq.raceNumber = raceNum;
                            detReq.racingCode = "G";
                            detReq.meetingName = venue;
                            detReq.meetingDate = today3;

                            try
                            {
                                detResp = raceServ.getRaceDetailsV2(vMeta, detReq);

                                if (detResp.meeting.raceDetail.status == "NORMAL")
                                {
                                    int meetingCode = Convert.ToInt32(detResp.meeting.meetingCode);

                                    raceDetail raceDet = detResp.meeting.raceDetail;
                                    raceSelectionDetail[] runners = raceDet.raceSelections;
                                    foreach (raceSelectionDetail runner in runners)
                                    {
                                        int idx = races.FindIndex(x => x.Box == runner.number);
                                        races[idx].VicPrice = runner.toteWinPrice;
                                        races[idx].VicPlace = runner.totePlacePrice;
                                        //                                    races[idx].meetingCode = Convert.ToInt32(MeetingCode[idx]);
                                        races[idx].meetingCode = meetingCode;
                                        races[idx].qldMeetingCode = QLDMeetingCode[idx];
                                        races[idx].MyPrice = Convert.ToDouble(MyPrice[idx].ToString());
                                        races[idx].Tip = Tip[idx].ToString();
                                        races[idx].BetAmt = Convert.ToDouble(MyBet[idx].ToString());
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error Process Race - VIC- " + ex.Message.ToString());
                            }

                            // Process NSW
                            raceServ = new RacingServiceClient();
                            wsTabRacing.apiMeta nMeta = new wsTabRacing.apiMeta();
                            vMeta.usernamePasswordToken = Globals.DefaultAuth;
                            vMeta.requestChannel = Globals.NswChan;
                            vMeta.jurisdictionId = Globals.NswJury;
                            vMeta.deviceId = Globals.DeviceID;
                            vMeta.deviceIdSpecified = true;
                            detReq = new raceDetailsRequestV2();
                            detResp = new raceDetailsResponseV2();
                            detReq.raceNumber = raceNum;
                            detReq.racingCode = "G";
                            detReq.meetingName = venue;
                            detReq.meetingDate = today3;

                            try
                            {
                                detResp = raceServ.getRaceDetailsV2(vMeta, detReq);

                                if (detResp.meeting.raceDetail.status == "NORMAL")
                                {
                                    int meetingCode = Convert.ToInt32(detResp.meeting.meetingCode);

                                    raceDetail raceDet = detResp.meeting.raceDetail;
                                    raceSelectionDetail[] runners = raceDet.raceSelections;
                                    foreach (raceSelectionDetail runner in runners)
                                    {
                                        int idx = races.FindIndex(x => x.Box == runner.number);
                                        races[idx].NswPrice = runner.toteWinPrice;
                                        races[idx].NswPlace = runner.totePlacePrice;
                                        races[idx].MyPrice = Convert.ToDouble(MyPrice[idx].ToString());
                                        races[idx].Tip = Tip[idx].ToString();
                                        races[idx].BetAmt = Convert.ToDouble(MyBet[idx].ToString());

                                    }
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Error Process Rave - NSW");
                            }

                            global.dbCon = new SqlConnection(Globals.dbConString);
                            global.dbCon.Open();

                            totalPct = 0;

                            foreach (RunningRace race in races)
                            {
                                double bestWin = GetBestPrice(race.VicPrice, race.NswPrice, race.QldPrice);
								double pct;
								if (bestWin == 0)
								{
									// what do use as a percent when there is no best price?
									// also - under what conditions do we have no best price?
									pct = 0;
								}
								else
								{
									pct = Math.Round(100 / bestWin, 2);
									totalPct += pct;
								}

                                double bestPlace = GetBestPrice(race.VicPlace, race.NswPlace, race.QldPlace);
                                string data = "update LivePrices set QldPrice = " + race.QldPrice 
									+ ", VicPrice = " + race.VicPrice + ", NswPrice = " + race.NswPrice 
									+ ", VicPlace = " + race.VicPlace + ", NswPlace = " + race.NswPlace 
									+ ", QldPlace = " + race.QldPlace + ", BestWin = " + bestWin 
									+ ", BestPlace = " + bestPlace + ", BestPct = " + pct 
									+ ", RaceStatus = '" + race.Status + "' where RaceID = '" + raceID 
									+ "' and RaceNum = '" + raceNum + "' and Box = " + race.Box;
                                SqlCommand cmdupd = new SqlCommand(data, global.dbCon);
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

                            if ((response.Data[0].Meetings[0].Races[0].Status == "CLOSED") || (response.Data[0].Meetings[0].Races[0].Status == "ABANDONED") || (response.Data[0].Meetings[0].Races[0].Status == "PAYING"))
                            {
                                ready = false;
                            }

                            //                        if (response.Data[0].Meetings[0].Races[0].RaceTime.AddHours(1).ToString("hh:mm:ss") == DateTime.Now.AddSeconds(Globals.offset).ToString("hh:mm:ss")) // bet here - Daylight Savings
//                            if (response.Data[0].Meetings[0].Races[0].RaceTime.ToString("hh:mm:ss") == DateTime.Now.AddSeconds(Globals.offset).ToString("hh:mm:ss")) // bet here
                            if (response.Data[0].Meetings[0].Races[0].RaceTime <= DateTime.Now.AddSeconds(Globals.offset)) // bet here
                            {
                                Console.WriteLine("Betting Now");

                                foreach (RunningRace race in races)
                                {
                                    AutoBet(race);
                                    race.NswPrice = 0;
                                    race.VicPrice = 0;
                                    race.QldPrice = 0;
                                }
                                global.dbCon.Close();
                                ready = false;
                                Globals.RaceThread.TryRemove(raceID, out thisThread);
                                Console.WriteLine("Finished Thread:" + raceID);
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error Processing Top Level - " + e.Message.ToString());
                    }
                }
            }
        }

        private void AutoBet(RunningRace race)
        {
            ProcessAutoBet autoBet = new ProcessAutoBet();

            double bestWin = GetBestPrice(race.VicPrice, race.NswPrice, race.QldPrice);
            double pct = Math.Round(100 / bestWin, 2);

            autoBet.pctOn = Globals.PctOn;
            autoBet.useRatingBetAmount = useRatingBetAmount;
            autoBet.betOnVic = betOnVic;
            autoBet.betOnNsw = betOnNsw;
            autoBet.betOnQld = betOnQld;
            autoBet.betPct = Globals.BetPct;
            autoBet.betToWin = betToWin;
            autoBet.betOnAll = betOnAll;
            autoBet.Venue = venue;
//            autoBet.QLDPlace = 
            autoBet.bestPrice = bestWin;
            //autoBet.bestPct = pct;
            autoBet.bestPct = totalPct;
            autoBet.vicPrice = race.VicPrice;
            autoBet.nswPrice = race.NswPrice;
            autoBet.qldPrice = race.QldPrice;
            autoBet.myPrice = race.MyPrice;
            autoBet.ratingBetAmt = race.BetAmt;
            //autoBet.calAmt = 0;
            autoBet.selection = race.Box;
            autoBet.runner = race.Dog;
            autoBet.raceID = raceID;
            autoBet.meetingCode = race.meetingCode;
            autoBet.qldMeetingCode = race.qldMeetingCode;
            autoBet.raceNum = race.RaceNum;
            autoBet.QLDPlace = race.QldPlace;
            autoBet.NSWPlace = race.NswPlace;
            autoBet.VICPlace = race.VicPlace;
            autoBet.txtStatus = txtStatus;

            try
            {
                autoBet.tip = race.Tip.ToString();
            }
            catch (Exception)
            {
                autoBet.tip = "1";
            }

            autoBet.ProcessAutoBetNow();

        }

        private double GetBestPrice(double d1, double d2, double d3)
        {
            double d = 0.0;
            if (d1 > d) d = d1;
            if (d2 > d) d = d2;
            if (d3 > d) d = d3;
            return d;
        }

        ~ProcessRace()
        {
            this.Dispose(false);
        }

        private bool isDisposed = false;
        public void Dispose()
        {
            if (!isDisposed)
                Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            isDisposed = true;
            if (disposing)
            {
                this.Dispose();
            }
        }
    }
}
