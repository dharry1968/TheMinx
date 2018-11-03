using System;
using System.Collections.Generic;
using System.Linq;
using TheMinx.Tatts.Data.Data.Tote;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TheMinx.wsTabBetting;
using System.Data.SqlClient;
using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;

namespace TheMinx
{
    public class Bet
    {
        Utils utils = new Utils();
        Globals global = new Globals();
        string live = "No";

        public string RaceID { get; set; }
        public int Box { get; set; }
        public string Runner { get; set; }
        public double QLDPrice { get; set; }
        public double NSWPrice { get; set; }
        public double VICPrice { get; set; }
        public double QLDPlace { get; set; }
        public double NSWPlace { get; set; }
        public double VICPlace { get; set; }
        public string Tip { get; set; }
        public double BestWin { get; set; }
        public double BestPlace { get; set; }
        public double BestPct { get; set; }
        public double MyPrice { get; set; }
        public double MyBetAmt { get; set; }
        public string Venue { get; set; }
        public KryptonRichTextBox txtStatus;


        public void PlaceBetQld(string RaceID, int selection, double betamt, int racenum, string meetingcode)
        {
            ToteBet bet = new ToteBet();
            bet.BetType = "WP"; //Win Only - for WIn and Place code is WP
            bet.MeetingCode = meetingcode;
            bet.RaceNumber = racenum;
            ToteSelection sel1 = GetToteSelection(selection.ToString());
            bet.Selections.Add(sel1);
            betamt = betamt * 2;
            betamt = Math.Round(betamt, MidpointRounding.AwayFromZero);
            betamt = betamt / 2;
            if (Globals.BetType == "WN")
            {
                bet.WinInvestment = betamt;
                bet.PlaceInvestment = 0;
            }

            if (Globals.BetType == "EW")
            {
                bet.WinInvestment = betamt;
                bet.PlaceInvestment = betamt;
            }

            if (Globals.BetType == "PL")
            {
                bet.WinInvestment = 0;
                bet.PlaceInvestment = betamt;
            }

            
            ToteBetRequest request = new ToteBetRequest();
//            request.AuthenticationToken = Globals.QldAuth;
//            request.SessionId = Globals.QldAuth;
            request.CustomerSession.SessionId = Globals.QldAuth;

            request.Bets.Add(bet);
            if (Globals.BettingEnabled)
            {
//                request.Sell = true;
                live = "Yes";
            }
            else
            {
//                request.Sell = false;
                bet.WinInvestment = 0;
                bet.PlaceInvestment = 0;
                live = "No";
            }

            string requestJSON = JsonConvert.SerializeObject(request);
            string responseJSON = TattsUtils.CallAPI2("tote/bet/", requestJSON, true, "POST");

            ToteBetResponse response = null;

            if (!string.IsNullOrEmpty(responseJSON))
            {
                response = JsonConvert.DeserializeObject<ToteBetResponse>(responseJSON);
            }

            if (response != null && response.Bets.Count > 0)
            {
                ToteTicket ticket = response.Bets[0].Ticket;
                if (ticket.Summary == "Invalid Meeting Code")
                {
                    Console.WriteLine("Invalid Meeting Code:" + meetingcode);

                }

                WriteStatus(string.Format("[Success QLD: Live:{0} {1}, Actual Cost: ${2}] {3} {4} Runner:{5}",live , ticket.Success, ticket.ActualSpend, ticket.Summary, DateTime.Now.ToString("hh:MM:ss"),selection.ToString().Replace("'","")), Globals.BettingEnabled, RaceID, selection.ToString(), "QLD", "BetStatus",betamt.ToString());
                utils.WriteLog(string.Format("[Success QLD: Live:{0} {1}, Actual Cost: ${2}] {3} Runner:{4} Race ID:{5} Time:{6}", live, ticket.Success, ticket.ActualSpend, ticket.Summary, selection.ToString().Replace("'", ""),RaceID,DateTime.Now.ToString("hh:mm:ss")));
                string betstatus = string.Format("[Success QLD: Live:{0} {1}, Actual Cost: ${2}] {3} {4} Runner:{5}",live , ticket.Success, ticket.ActualSpend, ticket.Summary, DateTime.Now.ToString("hh:MM:ss"),selection.ToString().Replace("'",""));
                WriteBet(RaceID, DateTime.Now.ToString("yyyyMMdd"), Venue, racenum, Box, Runner, QLDPrice, NSWPrice, VICPrice, QLDPlace, NSWPlace, VICPlace, Tip.ToString(), BestWin, BestPlace, BestPct, betstatus, MyPrice, MyBetAmt, "QLD", 0, 0, 0, 0, 0, 0, Globals.BettingEnabled);
                txtStatus.Invoke(new MethodInvoker(delegate
                {
                    txtStatus.AppendText(string.Format("[Success QLD: Live:{0} {1}, Actual Cost: ${2}] {3} Runner: {4} Venue: {5} Time: {6}", live, ticket.Success, ticket.ActualSpend, ticket.Summary, selection.ToString().Replace("'", ""), Venue, DateTime.Now.ToString("hh:mm:ss")));
                    txtStatus.AppendText("\n");
                }));
            }
            else
            {
                WriteStatus("Bet Failed QLD (Unknown Reason)", Globals.BettingEnabled, RaceID, selection.ToString(), "QLD", "BetStatus","");
                txtStatus.Invoke(new MethodInvoker(delegate
                {
                    txtStatus.AppendText(string.Format("[Bet Failed QLD: Unknown Reason]"));
                    txtStatus.AppendText("\n");
                }));

                WriteBet(RaceID, DateTime.Now.ToString("yyyyMMdd"), Venue, racenum, Box, Runner, QLDPrice, NSWPrice, VICPrice, QLDPlace, NSWPlace, VICPlace, Tip.ToString(), BestWin, BestPlace, BestPct, response.Message.ToString(), MyPrice, MyBetAmt, "QLD", 0, 0, 0, 0, 0, 0, Globals.BettingEnabled);

            }
            Console.WriteLine("QLD After Bet:" + RaceID + " - " + DateTime.Now.ToString("hh:MM:ss"));
            utils.GetQldBal();
        }

        protected ToteSelection GetToteSelection(string selectionsString)
        {
            ToteSelection selections = new ToteSelection();

            selections.Field = false;
            selections.Runners = new List<int>();

            selectionsString = Regex.Replace(selectionsString, @"\s+", " ");
            selectionsString = selectionsString.Replace(" ", ",");
            selectionsString = selectionsString.Replace("/", ",");
            selectionsString = selectionsString.Replace(@"\", ",");

            string[] runners = selectionsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string runner in runners)
            {
                if (runner.ToUpper() == "F")
                {
                    selections.Field = true;
                    selections.Runners = new List<int>();

                    break;
                }
                else
                {
                    int runnerNumber = ParserUtils.String_GetIntValue(runner, 0);

                    if (runnerNumber > 0 && runnerNumber <= 24)
                    {
                        selections.Runners.Add(runnerNumber);
                    }
                }
            }

            if (selections.Field || selections.Runners.Count >= 1)
            {
                return selections;
            }
            else
            {
                return null;
            }
        }

		/// <summary>
		/// A record of a bet made
		/// </summary>
		class BetRecord
		{
			public string RaceId;
			public string Runner;
		}

		/// <summary>
		/// Keep a history of bets made, to avoid placing a second bet on the same runner and race
		/// </summary>
		List<BetRecord> betHistory = new List<BetRecord>();

        public void PlaceBetVic(string RaceID, int selection, double betamt, int racenum, int meetingcode, string Runner)
        {
			// ensure we don't bet twice on the same runner in the same race
			var prevBet = betHistory.FirstOrDefault(b => b.RaceId == RaceID && b.Runner == Runner);
			if (prevBet != null)
				return;

			betHistory.Add(new BetRecord { RaceId = RaceID, Runner = Runner });
            Console.WriteLine("Placed Bet VIC");

            apiMeta meta = new apiMeta();
            meta.deviceId = Globals.DeviceID;
            meta.requestChannel = Globals.VicChan;
            meta.jurisdictionId = Globals.VicJury;
            meta.deviceIdSpecified = true;
            meta.usernamePasswordToken = Globals.VicAuth;
            bool yesterday = false;
            TimeSpan tme = DateTime.Now.TimeOfDay;
            if (tme.Ticks < 26000)
            {
                yesterday = true;
            }

            wsTabBetting.BettingServiceClient bet = new BettingServiceClient();

            //            MessageViewerInspector msgViewer = new MessageViewerInspector();
            //            bet.Endpoint.Behaviors.Add(msgViewer);

            bettingRequest betReq = new bettingRequest();
            betDetailsReq betDetReq = new betDetailsReq();

            List<legDetailsReq> leglist = new List<legDetailsReq>();
            List<betSelection> sellist = new List<betSelection>();
            List<betAmount> betamtlist = new List<betAmount>();
            List<betDetailsReq> betdetaillist = new List<betDetailsReq>();

            betSelection sel = new betSelection();
            sel.selectionNumber = selection.ToString();
            sel.selectionName = Runner;
            sel.selectionSeparator = " ";
            sellist.Add(sel);

            legDetailsReq leg = new legDetailsReq();
            if (Globals.BetType == "WN") leg.prodCode = 1;
            if (Globals.BetType == "EW") leg.prodCode = 12;
            if (Globals.BetType == "PL") leg.prodCode = 2;
//            leg.prodCode = 1; // 12 for each way - 1 for Win only
            leg.propositionNumber = 0;
            leg.raceNumber = racenum;
            leg.selectionList = sellist.ToArray();
            leglist.Add(leg);

            betAmount betAmt = new betAmount();
            betamt = betamt * 2;
            betamt = Math.Round(betamt, MidpointRounding.AwayFromZero);
            betamt = betamt / 2;
            betAmt.amountInvested = betamt;
            //            betAmt.amountInvested = 5;
            betAmt.returnsPerBet = "0";
            betamtlist.Add(betAmt);

            betDetReq.betType = "Parimutuel";
            betDetReq.betAmountList = betamtlist.ToArray();
            betDetReq.legList = leglist.ToArray();
            betDetReq.allUpFormula = "0";
            betDetReq.acceptPartial = 0;
            betDetReq.accumulatorBet = false;
            betDetReq.betRefId = 0;
            //            betDetReq.scheduledType = 1;
            betDetReq.fixedOddsProdCode = 0;
            betDetReq.flexiBet = false;
            betDetReq.mystery = false;
            betDetReq.notifyMethod = 0;
            betDetReq.ordinalNumber = 1;
            betDetReq.scheduledType = 3;

            betDetReq.meetingCode = meetingcode;

            if (yesterday)
            {
                betDetReq.meetingDate = DateTime.Now.AddDays(-1).Date;
            }
            else
            {
                betDetReq.meetingDate = DateTime.Now.Date;
            }
            betDetReq.meetingDateSpecified = true;
            betDetReq.multiParlayFormula = null;
            betdetaillist.Add(betDetReq);

            betReq.betDetailsRequestList = betdetaillist.ToArray();

            bettingResponse resp = null;

            if (Globals.BettingEnabled)
            {
                resp = bet.placeBet(meta, betReq);
                live = "Yes";
            }
            else
            {
                resp = bet.validateBet(meta, betReq);
                live = "No";
            }

            string response = resp.betDetailsResponseList[0].statusMessage;

            if (response == "SUCCESS")
            {
                WriteStatus(string.Format("[Success VIC: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} {4} Runner: {5}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, DateTime.Now.ToString("hh:MM:ss"),Runner.Replace("'","")), Globals.BettingEnabled, RaceID, selection.ToString(), "VIC", "BetStatus",betamt.ToString());
                utils.WriteLog(string.Format("[Success VIC: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} Runner: {4} Race ID:{5} Time:{6}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, Runner.Replace("'", ""),RaceID,DateTime.Now.ToString("hh:mm:ss")));
                string betstatus = string.Format("[Success VIC: Live:{0} {1}, Actual Cost: ${2}] {3} {4} Runner:{5}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, DateTime.Now.ToString("hh:MM:ss"), Runner.Replace("'", ""));
                WriteBet(RaceID, DateTime.Now.ToString("yyyyMMdd"), Venue, racenum, Box, Runner, QLDPrice, NSWPrice, VICPrice, QLDPlace, NSWPlace, VICPlace, Tip.ToString(), BestWin, BestPlace, BestPct, betstatus, MyPrice, MyBetAmt, "VIC", 0, 0, 0, 0, 0, 0, Globals.BettingEnabled);
                txtStatus.Invoke(new MethodInvoker(delegate
                {
                    txtStatus.AppendText(string.Format("[Success VIC: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} Runner: {4} Venue: {5} Time: {6}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, Runner.Replace("'", ""), Venue, DateTime.Now.ToString("hh:mm:ss")));
                    txtStatus.AppendText("\n");
                }));
            }
            else
            {
                utils.WriteLog(string.Format("Bet Failed VIC - {0}", resp.betDetailsResponseList[0].statusMessage));
                WriteStatus(string.Format("[Bet Failed VIC: {0}]", resp.betDetailsResponseList[0].statusMessage), Globals.BettingEnabled, RaceID, selection.ToString(), "VIC", "BetStatus","");
                utils.WriteLog(string.Format("[Bet Failed VIC: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} Runner: {4} Race ID:{5} Time:{6}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, Runner.Replace("'", ""), RaceID, DateTime.Now.ToString("hh:mm:ss")));
                WriteBet(RaceID, DateTime.Now.ToString("yyyyMMdd"), Venue, racenum, Box, Runner, QLDPrice, NSWPrice, VICPrice, QLDPlace, NSWPlace, VICPlace, Tip.ToString(), BestWin, BestPlace, BestPct, resp.betDetailsResponseList[0].statusMessage, MyPrice, MyBetAmt, "VIC", 0, 0, 0, 0, 0, 0, Globals.BettingEnabled);
                txtStatus.Invoke(new MethodInvoker(delegate
                    {
                        txtStatus.AppendText((string.Format("[Bet Failed VIC: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} Runner: {4} Venue:{5} Time:{6}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, Runner.Replace("'", ""), Venue, DateTime.Now.ToString("hh:mm:ss"))));
                    }));
            }
            utils.GetVicBal();
        }

        public void PlaceBetNsw(string RaceID, int selection, double betamt, int racenum, int meetingcode, string Runner)
        {
            Console.WriteLine("Placed Bet NSW");

            apiMeta meta = new apiMeta();
            meta.deviceId = Globals.DeviceID;
            meta.requestChannel = Globals.NswChan;
            meta.jurisdictionId = Globals.NswJury; ;
            meta.deviceIdSpecified = true;
            meta.usernamePasswordToken = Globals.NswAuth;
            bool yesterday = false;
            TimeSpan tme = DateTime.Now.TimeOfDay;
            if (tme.Ticks < 26000)
            {
                yesterday = true;
            }

            wsTabBetting.BettingServiceClient bet = new BettingServiceClient();

            //            MessageViewerInspector msgViewer = new MessageViewerInspector();
            //            bet.Endpoint.Behaviors.Add(msgViewer);

            bettingRequest betReq = new bettingRequest();
            betDetailsReq betDetReq = new betDetailsReq();

            List<legDetailsReq> leglist = new List<legDetailsReq>();
            List<betSelection> sellist = new List<betSelection>();
            List<betAmount> betamtlist = new List<betAmount>();
            List<betDetailsReq> betdetaillist = new List<betDetailsReq>();

            betSelection sel = new betSelection();
            sel.selectionNumber = selection.ToString();
            sel.selectionName = Runner;
            sel.selectionSeparator = " ";
            sellist.Add(sel);

            legDetailsReq leg = new legDetailsReq();
            if (Globals.BetType == "WN") leg.prodCode = 1;
            if (Globals.BetType == "EW") leg.prodCode = 12;
            if (Globals.BetType == "PL") leg.prodCode = 2;
//            leg.prodCode = 1; //12 for eachway, 1 for win only
            leg.propositionNumber = 0;
            leg.raceNumber = racenum;
            leg.selectionList = sellist.ToArray();
            leglist.Add(leg);

            betAmount betAmt = new betAmount();
            betamt = betamt * 2;
            betamt = Math.Round(betamt, MidpointRounding.AwayFromZero);
            betamt = betamt / 2;

            betAmt.amountInvested = betamt;
            //            betAmt.amountInvested = 5;
            betAmt.returnsPerBet = "0";
            betamtlist.Add(betAmt);

            betDetReq.betType = "Parimutuel";
            betDetReq.betAmountList = betamtlist.ToArray();
            betDetReq.legList = leglist.ToArray();
            betDetReq.allUpFormula = "0";
            betDetReq.acceptPartial = 0;
            betDetReq.accumulatorBet = false;
            betDetReq.betRefId = 0;
            betDetReq.scheduledType = 3;
            betDetReq.fixedOddsProdCode = 0;
            betDetReq.flexiBet = false;
            betDetReq.mystery = false;
            betDetReq.notifyMethod = 0;
            betDetReq.ordinalNumber = 1;
            betDetReq.meetingCode = meetingcode;
            if (yesterday)
            {
                betDetReq.meetingDate = DateTime.Now.AddDays(-1).Date;
            }
            else
            {
                betDetReq.meetingDate = DateTime.Now.Date;
            }
            betDetReq.meetingDateSpecified = true;
            betdetaillist.Add(betDetReq);

            betReq.betDetailsRequestList = betdetaillist.ToArray();

            bettingResponse resp = null;
            if (Globals.BettingEnabled)
            {
                resp = bet.placeBet(meta, betReq);
                live  = "Yes";
            }
            else
            {
                resp = bet.validateBet(meta, betReq);
                live = "No";
            }

            string response = resp.betDetailsResponseList[0].statusMessage;

            if (response == "SUCCESS")
            {
                Console.WriteLine(string.Format("[Success NSW: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} Runner: {4}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance,Runner));
                WriteStatus(string.Format("[Success NSW: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} {4} Runner: {5}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, DateTime.Now.ToString("hh:MM:ss"),Runner.Replace("'","")), Globals.BettingEnabled, RaceID, selection.ToString(), "NSW", "BetStatus",betamt.ToString());
                utils.WriteLog(string.Format("[Success NSW: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} Runner: {4} Race ID:{5} Time:{6}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, Runner.Replace("'", ""), RaceID, DateTime.Now.ToString("hh:mm:ss")));
                string betstatus = string.Format("[Success NSW: Live:{0} {1}, Actual Cost: ${2}] {3} {4} Runner:{5}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, DateTime.Now.ToString("hh:MM:ss"), Runner.Replace("'", ""));
                WriteBet(RaceID, DateTime.Now.ToString("yyyyMMdd"), Venue, racenum, Box, Runner, QLDPrice, NSWPrice, VICPrice, QLDPlace, NSWPlace, VICPlace, Tip.ToString(), BestWin, BestPlace, BestPct, betstatus, MyPrice, MyBetAmt, "NSW", 0, 0, 0, 0, 0, 0, Globals.BettingEnabled);
                txtStatus.Invoke(new MethodInvoker(delegate
                {
                    txtStatus.AppendText(string.Format("[Success NSW: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} Runner: {4} Venue: {5} Time: {6}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, Runner.Replace("'", ""), Venue, DateTime.Now.ToString("hh:mm:ss")));
                    txtStatus.AppendText("\n");
                }));
            }
            else
            {
                Console.WriteLine("Bet Failed NSW - {0}", resp.betDetailsResponseList[0].statusMessage);
                utils.WriteLog(string.Format("Bet Failed NSW - {0}", resp.betDetailsResponseList[0].statusMessage));
                WriteStatus(string.Format("[Bet Failed NSW: {0}]", resp.betDetailsResponseList[0].statusMessage), Globals.BettingEnabled, RaceID, selection.ToString(), "NSW", "BetStatus","");
                utils.WriteLog(string.Format("[Bet Failed NSW: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} Runner: {4} Race ID:{5} Time:{6}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, Runner.Replace("'", ""), RaceID, DateTime.Now.ToString("hh:mm:ss")));
                WriteBet(RaceID, DateTime.Now.ToString("yyyyMMdd"), Venue, racenum, Box, Runner, QLDPrice, NSWPrice, VICPrice, QLDPlace, NSWPlace, VICPlace, Tip.ToString(), BestWin, BestPlace, BestPct, resp.betDetailsResponseList[0].statusMessage, MyPrice, MyBetAmt, "NSW", 0, 0, 0, 0, 0, 0, Globals.BettingEnabled);
                
                txtStatus.Invoke(new MethodInvoker(delegate
                    {
                        txtStatus.AppendText((string.Format("[Bet Failed NSW: Live:{0} {1}, Actual Cost: ${2}] Balance: {3} Runner: {4} Venue:{5} Time:{6}", live, resp.betDetailsResponseList[0].statusMessage, resp.betDetailsResponseList[0].amountTicket, resp.betDetailsResponseList[0].accountBalance, Runner.Replace("'", ""), Venue, DateTime.Now.ToString("hh:mm:ss"))));
                    }));
            }
            utils.GetNswBal();
        }

        private void WriteStatus(string status, bool livebetting, string RaceID, string box, string State, string Field, string BetAmount)
        {
            string live = string.Empty;
            if (!livebetting)
            {
                live = "FALSE";
            }
            else
            {
                live = "TRUE";
            }
            
            global.dbCon = new SqlConnection(Globals.dbConString);
            string updstr = "update LivePrices set " + Field + " = '" + status.Replace("."," ") + "', BetAmount = '" + BetAmount + "', BetState = '" + State + "' where RaceID = '" + RaceID + "' and RaceDate = '" + DateTime.Now.ToString("yyyyMMdd") + "' and Box = '" + box + "'";
            global.dbCon.Open();
            SqlCommand upd = new SqlCommand(updstr, global.dbCon);
            upd.ExecuteNonQuery();
            global.dbCon.Close();
        }

        private void WriteBet(string raceId, string raceDate, string venue, int raceNum, int box, string runner, double qldPrice, double nswPrice, double vicPrice, double qldPlace, double nswPlace, double vicPlace, string tip, double BestWin, double BestPlace, double BestPct, string BetStatus, double MyPrice, double BetAmt, string State, double PayQLD, double PayNSW, double PayVIC, double PayQLDPlace, double PayNSWPlace, double PayVICPlace, bool livebetting)
        {
            string live = string.Empty;
            if (!livebetting)
            {
                live = "FALSE";
            }
            else
            {
                live = "TRUE";
            }
            global.dbCon = new SqlConnection(Globals.dbConString);
//            string updstr = "update Bets set RaceId = '" + raceId + "', RaceDate = '" + raceDate + "', Venue = '" + venue + ", RaceNum = " + raceNum + ", Box = " + box + ", Runner = '" + runner + "', QLDPrice = " + qldPrice + ", NswPrice = " + nswPrice + ", VicPrice = " + vicPrice + ", QLDPlace = " + qldPlace + ", NSWPlace = " + nswPlace + ", VICPlace = " + vicPlace + ", Tip = '" + tip + "', BestWin = " + BestWin + ", BestPlace = " + BestPlace + ", BestPct = " + BestPct + ", BetStatus = '" + BetStatus + "', MyPrice = " + MyPrice + ", MyBetAmt = " + BetAmt + ", BetState = '" + State + "', LiveBetting = '" + live + "'";
            string updstr = "insert into Bets (RaceId, RaceDate, Venue, RaceNum, Box, Runner, QLDPrice, NSWPrice, VICPrice, QLDPlace, NSWPlace, VICPlace, Tip, BestWin, BestPlace, BestPct, BetStatus, MyPrice, MyBetAmt, BetState, LiveBetting) values  ('" + raceId + "','" + raceDate + "','" + venue + "'," + raceNum + "," + box + ",'" + runner.Replace("'","") + "'," + qldPrice + "," + nswPrice + ","+vicPrice + "," + qldPlace + "," + nswPlace + "," + vicPlace + ",'" + tip + "',"+BestWin + ","+BestPlace+","+BestPct+",'" + BetStatus +"',"+MyPrice+"," +BetAmt + ",'"+State+"','" +live+"')";
            global.dbCon.Open();
            SqlCommand upd = new SqlCommand(updstr, global.dbCon);
            upd.ExecuteNonQuery();
            global.dbCon.Close();
        }
    }
}
