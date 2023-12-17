using Newtonsoft.Json;

namespace MetersSender.Common.Models
{
    /// <summary>
    ///     Модель виртуального дома со счетчиками.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class HouseModel
    {
        /// <summary>
        ///     Идентификатор дома.
        /// </summary>
        [JsonProperty("id", Required = Required.Default)]
        public long Id { get; set; }

        /// <summary>
        ///     Название дома.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        ///     Название сервиса источника данных.
        /// </summary>
        [JsonProperty("sourceServiceName", Required = Required.Default)]
        public string SourceServiceName { get; set; }

        /// <summary>
        ///     Название сервиса приемника данных.
        /// </summary>
        [JsonProperty("recepientServiceName", Required = Required.Default)]
        public string RecepientServiceName { get; set; }

        /// <summary>
        ///     Список счетчиков, привязанных к данному дому.
        /// </summary>
        [JsonProperty("meters", Required = Required.Always)]
        public List<MeterModel> Meters { get; set; }
    }
}
