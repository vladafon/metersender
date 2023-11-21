using Newtonsoft.Json;

namespace MetersSender.Common.Models
{
    public class ReadingModel
    {
        [JsonProperty("value", Required = Required.Always)]
        public decimal Value { get; set; }

        [JsonProperty("sendingDateTime", Required = Required.Always)]
        public DateTimeOffset SendingDateTime { get; set; }
    }
}
