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
    }
}
