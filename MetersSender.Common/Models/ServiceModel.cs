using Newtonsoft.Json;

namespace MetersSender.Common.Models
{
    /// <summary>
    ///    Модель с информацией о сервисе, используемом в интеграции.
    /// </summary>
    public class ServiceModel
    {
        /// <summary>
        ///     Название сервиса.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
    }
}
