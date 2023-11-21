using Newtonsoft.Json;

namespace MetersSender.Saures.Models
{
    internal class Error
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
