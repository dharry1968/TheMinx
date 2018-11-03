using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Meeting
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MeetingCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MeetingType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Abandoned { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string VenueName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string WeatherCondition { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? WeatherConditionLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TrackCondition { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TrackConditionLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TrackRating { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TattsRace> Races { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TattsPool> Pools { get; set; }
    }
}