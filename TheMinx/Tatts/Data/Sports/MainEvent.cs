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
    public class MainEvent
    {
        [JsonProperty]
        public long? MainEventId { get; set; }

        [JsonProperty]
        public string EventName { get; set; }

        [JsonProperty]
        public string VenueName { get; set; }

        [JsonProperty]
        public string LocationName { get; set; }

        [JsonProperty]
        public string CountryName { get; set; }

        [JsonProperty]
        public string CountryCode { get; set; }

        [JsonProperty]
        public int? SortOrder { get; set; }

        [JsonProperty]
        public string Status { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime EventStartTime { get; set; }

        [JsonProperty]
        public List<SubEvent> SubEvents { get; set; }
    }
}