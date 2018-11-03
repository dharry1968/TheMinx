using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Xml;
using System.Web;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    class ConfigurationResponse
    {
        [JsonProperty]
        public Boolean Live { get; set; }

        [JsonProperty]
        public string CurrentApplicationVersionNumber { get; set; }
        
        [JsonProperty]
        public Boolean ForceUpdate { get; set; }
        
        [JsonProperty]
        public Boolean AllowTransactions { get; set; }
        
        [JsonProperty]
        public string ResponsibleGamblingURL{ get; set; }

        [JsonProperty]
        public ConfigurationEndPoint ServiceEndPoints{ get; set; }

        [JsonProperty]
        public ConfigurationEndPoint ContentEndPoints { get; set; }

    }
}
