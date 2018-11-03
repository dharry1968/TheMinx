using System;
using System.Linq;
using Newtonsoft.Json;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CustomerSession : TransactionResponse
    {
        [JsonProperty]
        public string SessionId { get; set; }
    }
}
