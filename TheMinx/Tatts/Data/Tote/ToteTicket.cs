using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx.Tatts.Data.Data.Tote
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ToteTicket
    {
        [JsonProperty]
        public bool Success { get; set; }

        [JsonProperty]
        public string Summary { get; set; }

        [JsonProperty]
        public decimal ActualSpend { get; set; }

        [JsonProperty]
        public decimal PercentOfDiv { get; set; }

        [JsonProperty]
        public decimal FullDivCost { get; set; }
    }
}