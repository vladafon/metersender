using MetersSender.Common.Models;

namespace MetersSender.DataAccess.Repository
{
    /// <summary>
    ///     Определяет методы доступа к БД.
    /// </summary>
    public interface IDatabaseRepository
    {
        /// <summary>
        ///     Добавляет информацию о сервисе в БД.
        /// </summary>
        /// <param name="serviceModel">Модель с информацией о сервисе.</param>
        /// <returns>Идентификатор сервиса.</returns>
        Task<long> AddServiceAsync(ServiceModel serviceModel);

        /// <summary>
        ///     Получить информацию обо всех настроенных домах и счетчиках.
        /// </summary>
        /// <returns>Список домов и счетчиков.</returns>
        Task<List<HouseModel>> GetHousesWitMetersAsync();

        /// <summary>
        ///     Добавляет информацию о доме и его счетчиках.
        /// </summary>
        /// <param name="houseModel">Модель с информацией о доме и счетчиках.</param>
        /// <param name="sourceServiceId">Идентификатор сервиса источника.</param>
        /// <param name="recepientServiceId">Идентификатор сервиса приемника.</param>
        Task AddHouseAsync(HouseModel houseModel, long sourceServiceId, long recepientServiceId);

        /// <summary>
        ///     Получить информацию обо всех настроенных в доме счетчиках с последними переданными показаниями.
        /// </summary>
        /// <param name="houseId">Идентификатор дома.</param>
        /// <returns>Список счетчиков с показаниями.</returns>
        Task<List<MeterModel>> GetMetersWithLastReadingsAsync(long houseId);


        /// <summary>
        ///     Получить информацию об истории переданных показаний по счетчику.
        /// </summary>
        /// <param name="meterId">Идентификатор счетчика.</param>
        /// <returns>Список переданных показаний счетчика.</returns>
        Task<List<ReadingModel>> GetMeterReadingsHistoryAsync(long meterId);

        /// <summary>
        ///     Добавить информацию о переданном показании по счетчику.
        /// </summary>
        /// <param name="meterId">Идентификатор счетчика.</param>
        /// <param name="meterId">Показания счетчика.</param>
        /// <returns>Список переданных показаний счетчика.</returns>
        Task AddMeterReadingAsync(long meterId, decimal readingValue);
    }
}
