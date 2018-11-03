using System;
using System.Linq;
using Newtonsoft.Json;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class balance : TransactionResponse
    {
        [JsonProperty]
        public decimal SharedBalance { get; set; }

        [JsonProperty]
        public decimal LotteriesBalance { get; set; }

        [JsonProperty]
        public decimal WageringBalance { get; set; }

        [JsonProperty]
        public decimal TotalAvailableBalance { get; set; }

    }
}
