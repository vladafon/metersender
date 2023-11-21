using Newtonsoft.Json;

namespace MetersSender.Neodom.Models.Requests.Attributes
{
    internal class SwitchPropertyAttributes
    {
        [JsonProperty("account")]
        public string AccountId { get; set; }
    }
}
