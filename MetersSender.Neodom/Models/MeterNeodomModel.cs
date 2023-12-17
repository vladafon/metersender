using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class MeterNeodomModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("last")]
        public decimal LastReading { get; set; }
    }
}
