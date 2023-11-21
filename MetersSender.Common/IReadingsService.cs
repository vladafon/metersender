using MetersSender.Common.Models;

namespace MetersSender.Common
{
    /// <summary>
    ///     Сервис, занимающийся получением и отправкой данных показаний счетчиков.
    /// </summary>
    public interface IReadingsService
    {
        /// <summary>
        ///     Отправляет показания для всех счетчиков в доме.
        /// </summary>
        /// <param name="houseId">Идентификатор дома.</param>
        /// <returns></returns>
        Task SendAllMetersReadingsAsync(long houseId);

        /// <summary>
        ///     Получает последние переданные показания для всех счетчиков в доме.
        /// </summary>
        /// <param name="houseId">Идентификатор дома.</param>
        /// <returns></returns>
        Task<List<MeterModel>> GetAllMetersLastReadingsAsync(long houseId);

        /// <summary>
        ///     Получает историю переданных показания для выбранного счетчика.
        /// </summary>
        /// <param name="meterId">Идентификатор счетчика.</param>
        /// <returns></returns>
        Task<List<ReadingModel>> GetMeterReadingsHistoryAsync(long meterId);
    }
}
