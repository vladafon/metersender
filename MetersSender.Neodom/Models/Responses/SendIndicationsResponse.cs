using Newtonsoft.Json;

namespace MetersSender.Neodom.Models.Responses
{
    internal class SendIndicationsResponse : NeodomResponse
    {
        [JsonProperty("add_indications")]
        public string Status { get; set; }
    }
}
