using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DataResponse: Response
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool Live { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime LastUpdate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MeetingSummary> Data { get; set; }
    }
}