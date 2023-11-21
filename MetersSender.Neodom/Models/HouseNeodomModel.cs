using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class HouseNeodomModel
    {
        [JsonProperty("account")]
        public string AccountId { get; set; }

        [JsonProperty("checked")]
        public bool IsSelectedAsPrimary { get; set; }

        [JsonProperty("house")]
        public string Address { get; set; }
    }
}
