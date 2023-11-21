using Newtonsoft.Json;

namespace MetersSender.Neodom.Models.Responses
{
    internal class LoginResponse : NeodomResponse
    {
        [JsonProperty("login")]
        public string Status { get; set; }
    }
}
