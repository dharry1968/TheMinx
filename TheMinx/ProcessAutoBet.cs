using System;
using System.Collections.Generic;
using System.Linq;
using ComponentFactory.Krypton.Toolkit;

namespace TheMinx
{
    public class ProcessAutoBet
    {
        public bool pctOn { get; set; }
        public int raceNum { get; set; }
        public bool useRatingBetAmount { get; set; }
        public bool betOnVic { get; set; }
        public bool betOnNsw { get; set; }
        public bool betOnQld { get; set; }
        public bool betOnAll { get; set; }
        public double betPct { get; set; }
        public double betToWin { get; set; }
        public string Venue { get; set; }
        public double bestPrice { get; set; }
        public double bestPct { get; set; }
        public double vicPrice { get; set; }
        public double nswPrice { get; set; }
        public double qldPrice { get; set; }
        public double myPrice { get; set; }
        public double ratingBetAmt { get; set; }
        public double calAmt { get; set; }
        public int selection { get; set; }
        public string runner { get; set; }
        public string raceID { get; set; }
        public int meetingCode { get; set; }
        public string qldMeetingCode { get; set; }
        public string tip { get; set; }
        public double QLDPlace { get; set; }
        public double NSWPlace { get; set; }
        public double VICPlace { get; set; }
        public KryptonRichTextBox txtStatus;

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

		/// <summary>
		/// Uses a betting history to determine if we should place a bet on the given race and runner.
		/// If we have bet before on this race and runner, returns false.
		/// Otherwise, add a record of the bet and return true
		/// </summary>
		/// <param name="RaceID">the race to bet on</param>
		/// <param name="Runner">the runner in race</param>
		/// <returns>true if we should place the bet, else false because we've already made a bet on this race and runner</returns>
		private bool ShouldPlaceBet(string RaceID, string Runner)
		{
			// ensure we don't bet twice on the same runner in the same race
			bool prevBet = betHistory.FirstOrDefault(b => b.RaceId == RaceID && b.Runner == Runner) != null; 
			if (!prevBet)
				betHistory.Add(new BetRecord() { RaceId = RaceID, Runner = Runner });
			return !prevBet;
		}

        public void ProcessAutoBetNow()
        {
//            if (!Globals.BettingEnabled) return;

			// don't bet twice on same race and runner
			if (!ShouldPlaceBet(raceID, runner))
				return;

            decimal totalBal = Globals.VicBal + Globals.NswBal + Globals.QldBal;

            if (totalBal > Globals.StopWin) return;
            if (totalBal < (Globals.StopLoss * -1)) return;

            Bet bet = new Bet();
            bet.RaceID = raceID;
            bet.Venue = Venue;
            bet.Box = selection;
            bet.Runner = runner;
            bet.VICPrice = vicPrice;
            bet.NSWPrice = nswPrice;
            bet.QLDPrice = qldPrice;
            bet.VICPlace = VICPlace;
            bet.NSWPlace = NSWPlace;
            bet.QLDPlace = QLDPlace;
            bet.MyPrice = myPrice;
            bet.MyBetAmt = ratingBetAmt;
            bet.BestWin = bestPrice;
            bet.BestPct = bestPct;
            bet.txtStatus = txtStatus;

            if (tip == "0") tip = "";
            bet.Tip = tip;

            if (Globals.UseRatingBetAmt)//Use Price 
            {
				// if true, we should place bets where we can
				bool placeBet = false;

				// true if we should place a vic bet, etc
				bool betVic = false;
				bool betNsw = false;
				bool betQld = false;

                int valCnt = 0;

				// first, see if we should be betting anywhere
                if (vicPrice > myPrice && vicPrice > 0 && myPrice > 0 && tip != "")
                {
					placeBet = true;
                }
                else if (nswPrice > myPrice && nswPrice > 0 && myPrice > 0 && tip != "")
                {
					placeBet = true;
                }
                else if (qldPrice > myPrice && qldPrice > 0 && myPrice > 0 && tip != "")
                {
					placeBet = true;
                }

                if (vicPrice > myPrice && vicPrice > 0 && myPrice > 0 && tip != "")
                {
                    valCnt += 1;
                }

                if (nswPrice > myPrice && nswPrice > 0 && myPrice > 0 && tip != "")
                {
                    valCnt += 1;
                }

                if (qldPrice > myPrice && qldPrice > 0 && myPrice > 0 && tip != "")
                {
                    valCnt += 1;
                }

				// if anywhere should be betted on, then bet on each active tote
				if (placeBet && valCnt > 1 && (betOnVic || betOnAll) && Globals.VicEnable)
				{
                    if (useRatingBetAmount)
                    {
                        bet.PlaceBetVic(raceID, selection, ratingBetAmt, raceNum, meetingCode, runner);
                    }
                    else
                    {
                        calAmt = betToWin / (vicPrice - 1);
                        bet.PlaceBetVic(raceID, selection, calAmt, raceNum, meetingCode, runner);
                    }
				}

                if (placeBet && valCnt > 1 && (betOnNsw || betOnAll) && Globals.NswEnable)
				{
                    if (useRatingBetAmount)
                    {
                        bet.PlaceBetNsw(raceID, selection, ratingBetAmt, raceNum, meetingCode, runner);
                    }
                    else
                    {
                        calAmt = betToWin / (nswPrice - 1);
                        bet.PlaceBetNsw(raceID, selection, calAmt, raceNum, meetingCode, runner);
                    }

				}

                if (placeBet && valCnt > 1 && (betOnQld || betOnAll) && Globals.QldEnable)
				{
					if (useRatingBetAmount)
					{
						bet.PlaceBetQld(raceID, selection, ratingBetAmt, raceNum, qldMeetingCode);
					}
					else
					{
						calAmt = betToWin / (qldPrice - 1);
						bet.PlaceBetQld(raceID, selection, calAmt, raceNum, qldMeetingCode);
					}
				}
            }
            else//Use % Best Price applied
            {
/*                if (bestPct >= Globals.BetPct)
                {
					// if true, we should place bets where we can
                    bool placeBet = 
						   (vicPrice == bestPrice && vicPrice > 0)
						|| (nswPrice == bestPrice && nswPrice > 0)
						|| (qldPrice == bestPrice && qldPrice > 0);

					if (placeBet && Globals.VicEnable)
					{
						if (!useRatingBetAmount)
						{
							calAmt = betToWin / (vicPrice);
							bet.PlaceBetVic(raceID, selection, calAmt, raceNum, meetingCode, runner);
						}
						else
						{
							bet.PlaceBetVic(raceID, selection, ratingBetAmt, raceNum, meetingCode, runner);
						}
					}
					if (placeBet && Globals.NswEnable)
					{
                        if (!useRatingBetAmount)
                        {
                            calAmt = betToWin / (nswPrice);
                            bet.PlaceBetNsw(raceID, selection, calAmt, raceNum, meetingCode, runner);
                        }
                        else
                        {
                            bet.PlaceBetNsw(raceID, selection, ratingBetAmt, raceNum, meetingCode, runner);
                        }
					}
					if (placeBet && Globals.QldEnable)
					{
						if (!useRatingBetAmount)
                        {
                            calAmt = betToWin / (qldPrice);
                            bet.PlaceBetQld(raceID, selection, calAmt, raceNum, qldMeetingCode);
                        }
                        else
                        {
                            bet.PlaceBetQld(raceID, selection, ratingBetAmt, raceNum, qldMeetingCode);
                        }
					}
                }
 */
            }
        }
    }
}
