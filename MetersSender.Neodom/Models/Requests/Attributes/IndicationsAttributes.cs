using Newtonsoft.Json;

namespace MetersSender.Neodom.Models.Requests.Attributes
{
    internal class IndicationsAttributes
    {
        [JsonProperty("indications")]
        public string Indications { get; set; }
    }
}
