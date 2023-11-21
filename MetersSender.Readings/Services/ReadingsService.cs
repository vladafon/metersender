using MetersSender.Common;
using MetersSender.Common.Models;
using MetersSender.DataAccess.Repository;
using Microsoft.Extensions.Logging;

namespace MetersSender.Readings.Services
{
    ///<inheritdoc/>
    public class ReadingsService : IReadingsService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IRecepientIntegration _recepientIntegration;
        private readonly ISourceIntegration _sourceIntegration;
        private readonly ILogger<ReadingsService> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ReadingsService(IDatabaseRepository databaseRepository, 
            IRecepientIntegration recepientIntegration, 
            ISourceIntegration sourceIntegration,
            ILogger<ReadingsService> logger,
            IDateTimeProvider dateTimeProvider)
        {
            _databaseRepository = databaseRepository;
            _recepientIntegration = recepientIntegration;
            _sourceIntegration = sourceIntegration;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        ///<inheritdoc/>
        public async Task<List<MeterModel>> GetAllMetersLastReadingsAsync(long houseId) => 
            await _databaseRepository.GetMetersWithLastReadingsAsync(houseId);

        ///<inheritdoc/>
        public async Task<List<ReadingModel>> GetMeterReadingsHistoryAsync(long meterId) =>
            await _databaseRepository.GetMeterReadingsHistoryAsync(meterId);

        ///<inheritdoc/>
        public async Task SendAllMetersReadingsAsync(long houseId)
        {
            _logger.LogInformation($"Начата процедура отправки показаний счетчика для дома {houseId}.");

            var dateNow = _dateTimeProvider.Now.Date;

            if (dateNow.Day < 15 || dateNow.Day > 25)
            {
                throw new InvalidOperationException($"Передача показаний возможна только с 15 по 25 число месяца.");
            }

            var meters = await _databaseRepository.GetMetersWithLastReadingsAsync(houseId);

            foreach (var meter in meters)
            {
                var sourceMeterInfo = await _sourceIntegration.GetMeterByIdAsync(meter.SourceMeterId);

                if (sourceMeterInfo?.ReadingValue == null)
                {
                    throw new ArgumentNullException(nameof(sourceMeterInfo), $"Отсутствуют данные о текущем показании счетчика {meter.Id}");
                }

                await _recepientIntegration.SetMeterReadingAsync(meter.RecepientMeterId, sourceMeterInfo.ReadingValue.Value);

                await _databaseRepository.AddMeterReadingAsync(int.Parse(meter.Id), sourceMeterInfo.ReadingValue.Value);

                _logger.LogInformation($"Отправлены показания счетчика {meter.Id}, значение {sourceMeterInfo.ReadingValue.Value}.");
            }

            _logger.LogInformation($"Процедура отправки показаний счетчика для дома {houseId} завершена успешно.");
        }
    }
}
