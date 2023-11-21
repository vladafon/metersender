using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class ListMeter
    {
        [JsonProperty("water")]
        public List<MeterNeodomModel> WaterMeters { get; set; }

        [JsonProperty("electricity")]
        public List<MeterNeodomModel> ElectricityMeters { get; set; }
    }
}
