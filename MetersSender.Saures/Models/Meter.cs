using Newtonsoft.Json;

namespace MetersSender.Saures.Models
{
    internal class Meter
    {
        [JsonProperty("meter_id")]
        public int Id { get; set; }

        [JsonProperty("meter_name")]
        public string Name { get; set; }

        [JsonProperty("sn")]
        public string SerialNumber { get; set; }

        [JsonProperty("type")]
        public MeterType Type { get; set; }

        [JsonProperty("vals")]
        public List<decimal> Readings { get; set; }
    }
}
