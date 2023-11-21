using MetersSender.Api.Models;
using MetersSender.Common;
using MetersSender.Common.Models;

namespace MetersSender.Api.Readings
{
    ///<inheritdoc/>
    public class ReadingsApiService : IReadingsApiService
    {
        private readonly IReadingsService _readingsService;
        public ReadingsApiService(IReadingsService readingsService)
        {
            _readingsService = readingsService;
        }

        public async Task<Result<List<MeterModel>>> GetAllMetersLastReadingsAsync(long houseId)
        {
            var result = await _readingsService.GetAllMetersLastReadingsAsync(houseId);

            return new Result<List<MeterModel>> 
            { 
                Data = result 
            };
        }

        public async Task<Result<List<ReadingModel>>> GetMeterReadingsHistoryAsync(long meterId)
        {
            var result = await _readingsService.GetMeterReadingsHistoryAsync(meterId);

            return new Result<List<ReadingModel>>
            {
                Data = result
            };
        }

        public async Task<Result> SendAllMetersReadingsAsync(long houseId)
        {
            await _readingsService.SendAllMetersReadingsAsync(houseId);

            return new Result();
        }
    }
}
