using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx.Tatts.Data.Requests
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ChangePasswordRequest: TransactionRequest
    {
        [JsonProperty]
        public string OldPassword { get; set; }
        
        [JsonProperty]
        public string NewPassword { get; set; }
    }
}