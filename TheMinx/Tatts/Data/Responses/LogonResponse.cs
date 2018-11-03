using System;
using System.Linq;
using Newtonsoft.Json;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LogonResponse : TransactionResponse
    {
        [JsonProperty]
        public CustomerSession CustomerSession{ get; set; }

        [JsonProperty]
        public string CustomerReference { get; set; }

        [JsonProperty]
        public string Juristiction { get; set; }

        [JsonProperty]
        public string FirstName { get; set; }

        [JsonProperty]
        public string LastLogin { get; set; }

        [JsonProperty]
        public balance Balance { get; set; }

        [JsonProperty]
        public decimal TotalBalance { get; set; }

        [JsonProperty]
        public bool AccountLinkingEnabled { get; set; }

        [JsonProperty]
        public bool PasswordSuggestChange { get; set; }

        [JsonProperty]
        public bool PasswordExpired { get; set; }

        [JsonProperty]
        public string AcceptTCVersion { get; set; }

        [JsonProperty]
        public string RequiredTCVersion { get; set; }

        [JsonProperty]
        public bool IdentityVerified { get; set; }

        [JsonProperty]
        public Wagering Wagering { get; set; }

        [JsonProperty]
        public lotteries Lotteries { get; set; }

        [JsonProperty]
        public tattsonline TattaOnline { get; set; }

        [JsonProperty]
        public object CompositeErrorInfo { get; set; }

        [JsonProperty]
        public bool CompositeSuccess { get; set; }

        [JsonProperty]
        public ErrorInfo ErrorInfo { get; set; }

        [JsonProperty]
        public bool Success { get; set; }
       
    }
}