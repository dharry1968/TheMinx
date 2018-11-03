using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MeetingSummary
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime MeetingDate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CurrentDay { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Meeting> Meetings { get; set; }
    }
}
