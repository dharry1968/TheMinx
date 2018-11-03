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
    public class TattsTip
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Tips { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Tipster { get; set; }
    }
}