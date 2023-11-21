using Newtonsoft.Json;

namespace MetersSender.Api.Models
{
    public class Result<T> where T : class
    {
        /// <summary>
        ///     Ответ от сервиса.
        /// </summary>
        [JsonProperty("result")]
        public T Data { get; set; }
    }

    public class Result
    {
    }
}
