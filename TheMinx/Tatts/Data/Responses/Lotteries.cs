using System;
using System.Linq;
using Newtonsoft.Json;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class lotteries : TransactionResponse
    {
        [JsonProperty]
        public bool HasSubAccount { get; set; }

        [JsonProperty]
        public bool AttemptedLogin { get; set; }

        [JsonProperty]
        public bool IsLoggedIn{ get; set; }

        [JsonProperty]
        public logindetails LoginDetails { get; set; }

        [JsonProperty]
        public company Company { get; set; }

        [JsonProperty]
        public string PlayerCardNumber { get; set; }

        [JsonProperty]
        public ErrorInfo ErrorInfo { get; set; }

        [JsonProperty]
        public bool IsSelfExcluded { get; set; }

        [JsonProperty]
        public bool IsSelfBanned { get; set; }

        [JsonProperty]
        public string Jurisdiction { get; set; }


    }
}
