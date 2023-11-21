using Newtonsoft.Json;

namespace MetersSender.Saures.Models.Responses
{
    internal class SauresResponse<T> where T : class
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
