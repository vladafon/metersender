using MetersSender.Api.Models;
using MetersSender.Common.Models;

namespace MetersSender.Api.Readings
{
    /// <summary>
    ///     Предоставляет API методы для работы с показаниями счетчиков.
    /// </summary>
    public interface IReadingsApiService
    {
        /// <summary>
        ///     Отправляет показания для всех счетчиков в доме.
        /// </summary>
        /// <param name="houseId">Идентификатор дома.</param>
        /// <returns></returns>
        Task<Result> SendAllMetersReadingsAsync(long houseId);

        /// <summary>
        ///     Получает последние переданные показания для всех счетчиков в доме.
        /// </summary>
        /// <param name="houseId">Идентификатор дома.</param>
        /// <returns></returns>
        Task<Result<List<MeterModel>>> GetAllMetersLastReadingsAsync(long houseId);

        /// <summary>
        ///     Получает историю переданных показания для выбранного счетчика.
        /// </summary>
        /// <param name="meterId">Идентификатор счетчика.</param>
        /// <returns></returns>
        Task<Result<List<ReadingModel>>> GetMeterReadingsHistoryAsync(long meterId);
    }
}
