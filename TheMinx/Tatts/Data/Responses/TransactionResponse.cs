using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TransactionResponse: Response
    {
        [JsonProperty]
        public string AuthenticationToken { get; set; }

        [JsonProperty]
        public decimal Balance { get; set; }

        [JsonProperty]
        public decimal BatchCost { get; set; }

        public TransactionResponse()
        {
            this.AuthenticationToken = string.Empty;
            this.Balance = 0;
            this.BatchCost = 0;
        }
    }
}