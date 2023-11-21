using MetersSender.Api.Models;
using MetersSender.Common.Models;

namespace MetersSender.Api.Configuration
{
    /// <summary>
    ///     Предоставляет API методы для настройки сервиса.
    /// </summary>
    public interface IConfigurationApiService
    {
        /// <summary>
        ///     Получить список домов и счетчиков, настроенных в БД.
        /// </summary>
        /// <param name="houseModel">Модель с информацией о доме и счетчиках.</param>
        /// <returns></returns>
        Task<Result<List<HouseModel>>> GetHousesAsync();

        /// <summary>
        ///     Добавить дом и счетчики в БД.
        /// </summary>
        /// <returns></returns>
        Task<Result> AddHouseAsync(HouseModel houseModel);
    }
}
