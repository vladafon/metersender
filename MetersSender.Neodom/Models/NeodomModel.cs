using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class NeodomModel<T> where T : class
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("attributes")]
        public T Attributes { get; set; }
    }
}
