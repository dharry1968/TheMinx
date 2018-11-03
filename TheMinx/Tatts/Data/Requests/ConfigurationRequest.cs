using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ConfigurationRequest
    {
        [JsonProperty]
        public string ApplicationVersionNumber { get; set; }

        [JsonProperty]
        public string DeviceModelDI { get; set; }

        [JsonProperty]
        public string DeviceSystemVersion { get; set; }

        [JsonProperty]
        public string DeviceSystemName { get; set; }

        [JsonProperty]
        public string DeviceUniqueID { get; set; }
    }
}
