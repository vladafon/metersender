using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class IndicationsAttributes
    {
        [JsonProperty("indications")]
        public string Indications { get; set; }
    }
}
