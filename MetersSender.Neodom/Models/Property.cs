using Newtonsoft.Json;

namespace MetersSender.Neodom.Models
{
    internal class Property
    {
        [JsonProperty("objects")]
        public List<HouseNeodomModel> Houses { get; set; }

        [JsonProperty("type_code")]
        public string Type { get; set; }
    }
}
