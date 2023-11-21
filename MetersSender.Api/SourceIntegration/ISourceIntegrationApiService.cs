using MetersSender.Api.Models;
using MetersSender.Common.Models;

namespace MetersSender.Api.SourceIntegration
{
    /// <summary>
    ///     Предоставляет API методы для работы с источником данных.
    /// </summary>
    public interface ISourceIntegrationApiService
    {
        /// <summary>
        ///     Получает список счетчиков с их показаниями.
        /// </summary>
        /// <returns>Массив моделей с информацией о счетчиках.</returns>
        Task<Result<List<MeterModel>>> GetMetersAsync();
    }
}
