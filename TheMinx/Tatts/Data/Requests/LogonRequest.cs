using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LogonRequest
    {
        [JsonProperty]
        public string Username { get; set; }

        [JsonProperty]
        public string Password { get; set; }

        [JsonProperty]
        public string DeviceName { get; set; }
    }
}