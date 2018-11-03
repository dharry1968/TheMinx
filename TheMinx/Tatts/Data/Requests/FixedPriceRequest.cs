using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using TheMinx.Tatts.Data.Data.Tote;

namespace TheMinx.Tatts.Data.Requests
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FixedPriceOffer : TransactionRequest
    {
        [JsonProperty]
        public long OfferId { get; set; }

        [JsonProperty]
        public decimal WinReturn { get; set; }

        [JsonProperty]
        public decimal PlaceReturn { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class FixedPriceRequest: TransactionRequest
    {
        [JsonProperty]
        public bool Sell { get; set; }

        /// <summary>
        /// Old interface used by iPhone 1.0. Now depreciated. 
        /// </summary>
        [JsonProperty]
        public List<long> PropositionNumbers { get; set; }

        /// <summary>
        /// Android used this more intelligent interface instead of PropositionNumbers above.
        /// </summary>
        [JsonProperty]
        public List<FixedPriceOffer> Offers { get; set; }

        [JsonProperty]
        public int WinInvestment { get; set; }

        [JsonProperty]
        public int PlaceInvestment { get; set; }
    }
}