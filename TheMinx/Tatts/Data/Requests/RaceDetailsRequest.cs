using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    class RaceDetailsRequest
    {
        [JsonProperty]
        public string year { get; set; }

        [JsonProperty]
        public string month { get; set; }

        [JsonProperty]
        public string day { get; set; }

        [JsonProperty]
        public string MeetingCode { get; set; }

        [JsonProperty]
        public string RaceNumber { get; set; }
    }
}
