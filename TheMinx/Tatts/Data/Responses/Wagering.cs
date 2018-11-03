using System;
using System.Linq;
using Newtonsoft.Json;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Wagering : TransactionResponse
    {
        [JsonProperty]
        public bool HasSubAccount { get; set; }

        [JsonProperty]
        public bool AttemtedLogin { get; set; }

        [JsonProperty]
        public bool IsLoggedIn { get; set; }

        [JsonProperty]
        public ErrorInfo LoginDetails { get; set; }

        [JsonProperty]
        public bool IsSelfExcluded { get; set; }

        [JsonProperty]
        public bool IsSelfBanned { get; set; }

        [JsonProperty]
        public string Juristiction { get; set; }

    }
}