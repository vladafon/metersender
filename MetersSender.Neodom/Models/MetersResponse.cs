using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class MetersResponse
    {
        [JsonProperty("list_meter")]
        public ListMeter ListMeter { get; set; }
    }
}
