using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx.Tatts.Data.Data.Tote
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ToteBet
    {               
        [JsonProperty]
        public string BetType { get; set; }

        [JsonProperty]
        public string MeetingCode { get; set; }

        [JsonProperty]
        public bool IsPresale { get; set; }

        [JsonProperty]
        public int RaceNumber { get; set; }

        [JsonProperty]
        public bool IsRover { get; set; }

        [JsonProperty]
        public List<ToteSelection> Selections { get; set; }

        [JsonProperty]
        public double WinInvestment { get; set; }

        [JsonProperty]
        public double PlaceInvestment { get; set; }

        [JsonProperty]
        public ToteTicket Ticket { get; set; }

        public ToteBet()
        {
            this.BetType = string.Empty;
            this.MeetingCode = string.Empty;
            this.IsPresale = false;
            this.RaceNumber = 0;
            this.IsRover = false;
            this.Selections = new List<ToteSelection>();
            this.WinInvestment = 0;
            this.PlaceInvestment = 0;
            this.Ticket = null;
        }

        public bool IsMultiLeg()
        {
            bool response = false;

            string betType = this.BetType.ToUpper();

            response = (betType == "DD" ||
                        betType == "XD" ||
                        betType == "TT" ||
                        betType == "QD");

            return response;
        }
    }
}