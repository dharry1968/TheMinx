using System;
using System.Linq;
using Newtonsoft.Json;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ErrorInfo : TransactionResponse
    {
        [JsonProperty]
        public int SystemId { get; set; }

        [JsonProperty]
        public int ErrorNo { get; set; }

        [JsonProperty]
        public string DisplayMessage { get; set; }

        [JsonProperty]
        public bool ContactSupport { get; set; }

        [JsonProperty]
        public string SupportErrorReference { get; set; }


    }
}
