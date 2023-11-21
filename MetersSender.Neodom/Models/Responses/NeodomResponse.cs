using Newtonsoft.Json;

namespace MetersSender.Neodom.Models.Responses
{
    internal abstract class NeodomResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
