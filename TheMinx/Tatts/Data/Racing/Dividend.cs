using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Dividend
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DividendResult> DividendResults { get; set; }
    }
}