using Newtonsoft.Json;

namespace MetersSender.Common.Models
{
    /// <summary>
    ///     Модель с информацией о счетчике.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class MeterModel
    {
        /// <summary>
        ///     Идентификатор счетчика из БД.
        /// </summary>
        [JsonProperty("id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        ///     Название счетчика.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        ///     Текущее показание счетчика.
        /// </summary>
        [JsonProperty("readingValue", Required = Required.Default)]
        public decimal? ReadingValue { get; set; }

        /// <summary>
        ///     Дата отправки показания счетчика.
        /// </summary>
        [JsonProperty("readingSendingDateTime", Required = Required.Default)]
        public DateTimeOffset? ReadingSendingDateTime { get; set; }

        /// <summary>
        ///     Идентификатор счетчика в сервисе источнике.
        /// </summary>
        [JsonProperty("sourceMeterId", Required = Required.Default)]
        public string SourceMeterId { get; set; }

        /// <summary>
        ///     Идентификатор счетчика в сервисе приемнике.
        /// </summary>
        [JsonProperty("recepientMeterId", Required = Required.Default)]
        public string RecepientMeterId { get; set; }

    }
}
