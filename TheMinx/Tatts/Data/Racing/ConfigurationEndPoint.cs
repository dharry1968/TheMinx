using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Xml;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    class ConfigurationEndPoint
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string URL { get; set; }

    }
}
