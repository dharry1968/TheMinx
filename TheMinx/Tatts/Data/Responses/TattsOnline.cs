using System;
using System.Linq;
using Newtonsoft.Json;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class tattsonline : TransactionResponse
    {
        [JsonProperty]
        public string CustomerStatus { get; set; }

        [JsonProperty]
        public string ExternalCustomerId { get; set; }
    }
}