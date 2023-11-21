using Newtonsoft.Json;

namespace MetersSender.Neodom.Models.Responses
{
    internal class MyPropertyResponse : NeodomResponse
    {
        [JsonProperty("my_property")]
        public List<Property> Property { get; set; }
    }
}
