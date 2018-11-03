using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Response
    {
        [JsonProperty]
        public bool Success { get; set; }

        [JsonProperty]
        public int ErrorCode { get; set; }

        [JsonProperty]
        public string Message { get; set; }
    }
}