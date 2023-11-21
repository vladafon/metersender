using Newtonsoft.Json;

namespace MetersSender.Saures.Models
{
    internal class SauresObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("number")]
        public string ApartmentNumber { get; set; }

        [JsonProperty("label")]
        public string ApartmentType { get; set; }

        [JsonProperty("house")]
        public string ApartmentAddress { get; set; }
    }
}
