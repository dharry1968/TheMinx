using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using TheMinx.Tatts.Data.Data.Tote;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ToteBetResponse: TransactionResponse
    {
        [JsonProperty]
        public int TotalBets { get; set; }

        [JsonProperty]
        public int SuccessfulBets { get; set; }

        [JsonProperty]
        public int FailedBets { get; set; }

        [JsonProperty]
        public List<ToteBet> Bets { get; set; }

        public ToteBetResponse()
        {
            this.TotalBets = 0;
            this.SuccessfulBets = 0;
            this.FailedBets = 0;

            this.Bets = new List<ToteBet>();
        }
    }
}