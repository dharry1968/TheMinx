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
    public class Leg
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? LegNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? RaceNumber { get; set; }
    }
}