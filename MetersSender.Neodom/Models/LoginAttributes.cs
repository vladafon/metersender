using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class LoginAttributes
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
