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
    public class ToteBetRequest: TransactionRequest
    {
        [JsonProperty]
        public CustomerSession CustomerSession { get; set; }

        [JsonProperty]
        public List<ToteBet> Bets { get; set; }

        public ToteBetRequest()
        {
            this.Bets = new List<ToteBet>();
        }
    }
}