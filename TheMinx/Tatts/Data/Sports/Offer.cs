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
    public class Offer
    {
        [JsonProperty]
        public long? OfferId { get; set; }
        
        [JsonProperty]
        public string Status { get; set; }

        [JsonProperty]
        public string OfferName { get; set; }

        [JsonProperty]
        public int? SortOrder { get; set; }

        [JsonProperty]
        public bool PlaceBetting { get; set; }

        [JsonProperty]
        public int? PlayerNumber { get; set; }

        [JsonProperty]
        public int? HAD { get; set; }

        [JsonProperty]
        public decimal? WinReturn { get; set; }

        [JsonProperty]
        public decimal? PlaceReturn { get; set; }

        [JsonProperty]
        public decimal? ExtraValue1 { get; set; }

        [JsonProperty]
        public decimal? ExtraValue2 { get; set; }

        [JsonProperty]
        public bool? LateScratching { get; set; }
    }
}