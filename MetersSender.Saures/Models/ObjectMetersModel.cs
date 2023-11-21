using Newtonsoft.Json;

namespace MetersSender.Saures.Models
{
    internal class ObjectMetersModel
    {
        [JsonProperty("sensors")]
        public List<Sensor> Sensors { get; set; }
    }
}
