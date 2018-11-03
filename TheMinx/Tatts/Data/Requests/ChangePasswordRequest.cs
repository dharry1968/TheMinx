using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx.Tatts.Data.Requests
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreditCardDepositRequest: TransactionRequest
    {
        [JsonProperty]
        public string CardType { get; set; }
        
        [JsonProperty]
        public long CardNumber { get; set; }

        [JsonProperty]
        public short ExpiryMonth { get; set; }

        [JsonProperty]
        public short ExpiryYear { get; set; }

        [JsonProperty]
        public short CVV { get; set; }

        [JsonProperty]
        public int DepositAmount { get; set; }
    }
}