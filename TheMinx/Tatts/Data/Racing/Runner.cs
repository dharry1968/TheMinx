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
    public class TattsRunner
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? RunnerNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RunnerName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RiderName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? RiderChanged { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Barrier { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Box { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Weight { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Handicap { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DrawDetails { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LastThreeStarts { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Form { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Rating { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Scratched { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? LateScratching { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? WinOdds { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? WinOddsShortened { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LastWinOdds { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PlaceOdds { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? PlaceOddsShortened { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LastPlaceOdds { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? FPWinOdds { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? FPPlaceOdds { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? FPOfferId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? FPSuspended { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FPStatus { get; set; }
    }
}