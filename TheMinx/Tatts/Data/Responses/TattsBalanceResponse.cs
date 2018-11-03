using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Xml;

namespace TheMinx.Tatts.Data.Responses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TattsBalanceResponse : TransactionResponse
    {
        [JsonProperty]
        public string AuthenticationToken { get; set; }
        [JsonProperty]
        public decimal Balance { get; set; }
        [JsonProperty]
        public Boolean Success { get; set; }
        [JsonProperty]
        public int ErrorCode { get; set; }
        [JsonProperty]
        public string Message { get; set; }
    }
}
