using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TattsPool
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PoolType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Available { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Abandoned { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? JackpotIn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? JackpotOut { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PoolTotal { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Dividend> Dividends { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Leg> Legs { get; set; }

        public enum DividendRecordType
        {
            /// <summary>
            /// nodes: Meeting --> Pool --> MultiLeg --> DividendResult
            /// </summary>
            Mtg_Pool_MultiLeg_DividendResult,
            /// <summary>
            /// node: Meeting --> MultiPool --> Dividend --> DivResult (contains attribute RaceNo)
            /// 
            /// This data can appear and disappear depending on status of Pools, try to avoid
            /// </summary>
            Mtg_MultiPool_Dividend,
            /// <summary>
            /// nodes: Meeting --> Race --> Pool --> Dividend --> DivResult (missing  attribute RaceNo)
            /// </summary>
            Mtg_Race_Pool_Dividend
        }
    }
}