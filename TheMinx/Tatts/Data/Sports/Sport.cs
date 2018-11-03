using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Hotdogs.Tatts.Data.Data.Sports
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Sport
    {
        [JsonProperty]
        public long? SportId { get; set; }

        [JsonProperty]
        public string SportName { get; set; }

        [JsonProperty]
        public int? SortOrder { get; set; }

        [JsonProperty]
        public List<League> Leagues { get; set; }
    }
}