using Newtonsoft.Json;

namespace MetersSender.Saures.Models
{
    internal class LoginModel
    {
        [JsonProperty("sid")]
        public Guid Sid { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }

        [JsonProperty("api")]
        public int Api { get; set; }
    }
}
