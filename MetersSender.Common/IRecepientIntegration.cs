using MetersSender.Common.Models;


namespace MetersSender.Common
{
    /// <summary>
    ///     Определяет методы принимаещего сервиса.
    /// </summary>
    public interface IRecepientIntegration
    {
        /// <summary>
        ///     Получает информацию о принимающем сервисе.
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

        /// <summary>
        ///     Отправить показания счетчика в сервис.
        /// </summary>
        /// <param name="meterId">Внутренний идентификатор.</param>
        /// <param name="readingValue">Значение показания счетчика.</param>
        Task SetMeterReadingAsync(string meterId, decimal readingValue);
    }
}
