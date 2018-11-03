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
    public class Meeting
    {
        [JsonProperty]
        public long? MeetingId { get; set; }

        [JsonProperty]
        public string MeetingName { get; set; }

        [JsonProperty]
        public int? SortOrder { get; set; }

        [JsonProperty]
        public string MatchRoundSeason { get; set; }

        [JsonProperty]
        public List<MainEvent> MainEvents { get; set; }
    }
}