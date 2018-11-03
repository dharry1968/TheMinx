using System;
using System.Linq;
using Newtonsoft.Json;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class logindetails : TransactionResponse
    {
        [JsonProperty]
        public string[] AvailableProducts { get; set; }
    }
}