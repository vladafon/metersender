using Newtonsoft.Json;

namespace MetersSender.Common.Models
{
    /// <summary>
    ///     Модель с информацией о показании счетчика.
    /// </summary>
    public class ReadingModel
    {
        /// <summary>
        ///     Значение показания счетчика.
        /// </summary>
        [JsonProperty("value", Required = Required.Always)]
        public decimal Value { get; set; }

        /// <summary>
        ///     Дата и время отправки показания в UTC.
        /// </summary>
        [JsonProperty("sendingDateTime", Required = Required.Always)]
        public DateTimeOffset SendingDateTime { get; set; }
    }
}
