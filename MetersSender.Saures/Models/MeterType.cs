using Newtonsoft.Json;

namespace MetersSender.Saures.Models
{
    internal class MeterType
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
