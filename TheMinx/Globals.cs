using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;

namespace TheMinx
{
    class Globals
    {
        public SqlConnection dbCon { get; set; }
        public static long VicAcc { get; set; }
        public static string VicPwd { get; set; }
        public static long NswAcc { get; set; }
        public static string NswPwd { get; set; }
        public static long QldAcc { get; set; }
        public static string QldPwd { get; set; }
        public static bool VicEnable, QldEnable, NswEnable;
        public static string VicAuth { get; set; }
        public static string NswAuth { get; set; }
        public static string QldAuth { get; set; }
        public static decimal VicBal { get; set; }
        public static decimal NswBal { get; set; }
        public static decimal QldBal { get; set; }
        public static decimal TotalBal { get; set; }
        public static long VicJury = 1;
        public static long VicChan = 5;
        public static long NswJury = 2;
        public static long NswChan = 1;
        public static long DeviceID = 10000011;
        public static long DefaultAcc { get; set; }
        public static string DefaultPwd { get; set; }
        public static string DefaultAuth { get; set; }
        public static string dbConString = ConfigurationManager.ConnectionStrings["TheMinx.Properties.Settings.TheMinxConnectionString"].ToString();
        public static string ntgVenue;
        public static string ntgRace;
        public static int offset { get; set; }
        public static bool TodayLoaded { get; set; }
        public static bool BettingEnabled { get; set; }
        public static decimal StopWin { get; set; }
        public static decimal StopLoss { get; set; }
        public static string PreviousIndex { get; set; }
        public static bool DataLoaded { get; set; }
        public static double BetToWin { get; set; }
        public static bool PctOn { get; set; }
        public static double BetPct { get; set; }
        public static bool UseRatingBetAmt { get; set; }

        public static string BetType { get; set; }

        public static ConcurrentDictionary<string, Thread> RaceThread = new ConcurrentDictionary<string, Thread>();
        public static Dictionary<string,string> ViewingRace = new Dictionary<string,string>();
        public static ConcurrentDictionary<string, Thread> CurrentRace = new ConcurrentDictionary<string, Thread>();
        
    }
}
