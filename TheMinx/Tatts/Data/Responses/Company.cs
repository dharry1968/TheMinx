using System;
using System.Linq;
using Newtonsoft.Json;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class company : TransactionResponse
    {
        [JsonProperty]
        public string CompanyId { get; set; }

        [JsonProperty]
        public string CompanyDisplayName { get; set; }

        [JsonProperty]
        public string CompanyDescription { get; set; }

        [JsonProperty]
        public string CompanyUrl{ get; set; }

    }
}        
