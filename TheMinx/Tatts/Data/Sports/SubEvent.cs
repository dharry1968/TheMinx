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
    public class SubEvent
    {
        [JsonProperty]
        public long? SubEventId { get; set; }

        [JsonProperty]
        public string Status { get; set; }

        [JsonProperty]
        public int? SortOrder { get; set; }

        [JsonProperty]
        public int? OfferSortMethod { get; set; }

        [JsonProperty]
        public bool? LiveBetting { get; set; }

        [JsonProperty]
        public bool? PlaceBetting { get; set; }

        [JsonProperty]
        public long? BetTypeId { get; set; }

        [JsonProperty]
        public string BetTypeName { get; set; }

        [JsonProperty]
        public string BetTypeShortName { get; set; }

        [JsonProperty]
        public string BetTypeDesc { get; set; }

        [JsonProperty]
        public string DisplayComment { get; set; }

        [JsonProperty]
        public List<Offer> Offers { get; set; }
    }
}