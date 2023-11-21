using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class Indication
    {
        [JsonProperty("id")]
        public int MeterId { get; set; }

        [JsonProperty("value")]
        public decimal ReadingValue { get; set; }
    }
}
