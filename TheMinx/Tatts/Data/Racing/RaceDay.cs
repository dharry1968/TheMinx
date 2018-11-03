using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx.Tatts.Data.Data.Racing
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RaceDay
    {
        [JsonProperty]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime MeetingDate { get; set; }

        [JsonProperty]
        public bool? CurrentDay { get; set; }

        [JsonProperty]
        public List<Meeting> Meetings { get; set; }
    }
}