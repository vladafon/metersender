using Newtonsoft.Json;

namespace MetersSender.Saures.Models.Requests
{
    internal class LoginRequest
    {
        [JsonProperty("sid")]
        public Guid Sid { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }

        [JsonProperty("api")]
        public int Api { get; set; }
    }
}
