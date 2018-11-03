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
    public class TattsRace
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? RaceNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RaceName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime RaceTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Distance { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Abandoned { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? WeatherChanged { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string WeatherCondition { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? WeatherConditionLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? TrackChanged { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TrackCondition { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TrackConditionLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TrackRating { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? SubFavourite { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? TrackRatingChanged { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TattsRunner> Runners { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TattsPool> Pools { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TattsTip> Tips { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<TattsResult> Results { get; set; }
    }
}