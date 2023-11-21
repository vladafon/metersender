using Newtonsoft.Json;

namespace MetersSender.Saures.Models
{
    internal class Sensor
    {
        [JsonProperty("meters")]
        public List<Meter> Meters { get; set; }
    }
}
