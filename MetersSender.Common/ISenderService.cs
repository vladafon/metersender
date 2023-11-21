namespace MetersSender.Common
{
    /// <summary>
    ///     Сервис, занимающийся отправкой данных показаний счетчиков.
    /// </summary>
    public interface ISenderService
    {
        /// <summary>
        ///     Отправляет показания для всех счетчиков в доме.
        /// </summary>
        /// <param name="houseId">Идентификатор дома.</param>
        /// <returns></returns>
        Task SendAllMetersReadings(long houseId);
    }
}
