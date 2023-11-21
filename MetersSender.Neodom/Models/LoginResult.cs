using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class LoginResult
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("login")]
        public string LoginStatus { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
