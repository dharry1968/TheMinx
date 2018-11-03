using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx.Tatts.Data.Data.Tote
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ToteSelection
    {
        [JsonProperty]
        public bool Field { get; set; }

        [JsonProperty]
        public List<int> Runners { get; set; }

        public ToteSelection()
        {
            this.Field = false;
            this.Runners = new List<int>();
        }
    }
}