using Newtonsoft.Json;
namespace ShareMarketLiveUpdates.Functions.Models
{
    public class ShareInfo
    {
        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ask")]
        public decimal Ask { get; set; }

        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        [JsonProperty("ltp")]
        public decimal Ltp { get; set; }

        [JsonProperty("lastDayClosing")]
        public decimal LastDayClosing { get; set; }
        
        [JsonProperty("volume")]
        public int Volume { get; set; }
    }
}