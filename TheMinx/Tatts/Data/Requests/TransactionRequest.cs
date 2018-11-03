using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TransactionRequest
    {
//        [JsonProperty]
//        public string AuthenticationToken { get; set; }

        [JsonProperty]
        public string SessionId { get; set; }
    }
}