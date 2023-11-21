using Newtonsoft.Json;

namespace MetersSender.Saures.Models.Requests
{
    internal class ObjectMetersRequest
    {
        [JsonProperty("sensors")]
        public List<Sensor> Sensors { get; set; }
    }
}
