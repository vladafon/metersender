using Newtonsoft.Json;

namespace MetersSender.Neodom.Models.Responses
{
    internal class SwitchPropertyResponse : NeodomResponse
    {
        [JsonProperty("switch_property")]
        public string Status { get; set; }
    }
}
