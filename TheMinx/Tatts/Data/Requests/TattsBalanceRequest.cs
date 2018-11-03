using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web;


namespace TheMinx.Tatts.Data.Requests
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TattsBalanceRequest
    {
        [JsonProperty]
        public string AuthenticationToken { get; set; }

    }
}
