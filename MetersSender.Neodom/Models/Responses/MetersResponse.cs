using Newtonsoft.Json;

namespace MetersSender.Neodom.Models.Responses
{
    internal class MetersResponse : NeodomResponse
    {
        [JsonProperty("list_meter")]
        public ListMeter ListMeter { get; set; }
    }
}
