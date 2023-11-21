using MetersSender.Common.Models;


namespace MetersSender.Common
{
    /// <summary>
    ///     Определяет методы источника данных.
    /// </summary>
    public interface ISourceIntegration
    {
        /// <summary>
        ///     Получает информацию об источнике данных.
        /// </summary>
        /// <returns>Модель с информацией о сервисе.</returns>
        Task<ServiceModel> GetServiceInfoAsync();

        /// <summary>
        ///     Получает список счетчиков с их показаниями.
        /// </summary>
        /// <returns>Массив моделей с информацией о счетчиках.</returns>
        Task<List<MeterModel>> GetMetersAsync();

        /// <summary>
        ///     Получает актуальную информацию о счетчике по его внутреннему идентификатору.
        /// </summary>
        /// <param name="meterId">Внутренний идентификатор.</param>
        /// <returns>Модель с информацией о счетчике.</returns>
        Task<MeterModel> GetMeterByIdAsync(string meterId);
    }
}
