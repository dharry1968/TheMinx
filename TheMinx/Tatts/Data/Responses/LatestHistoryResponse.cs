using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TheMinx.Tatts.Data.Responses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LatestHistoryResponse: TransactionResponse
    {
        [JsonProperty]
        public List<HistoryRecord> HistoryRecords { get; set; }
    }
}