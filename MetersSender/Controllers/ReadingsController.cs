using MetersSender.Api.Models;
using MetersSender.Api.Readings;
using MetersSender.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetersSender.Controllers
{
    [ApiController]
    [Route("api/v1/readings")]
    public class ReadingsController : ControllerBase
    {
        private readonly IReadingsApiService _readingsApiService;
        public ReadingsController(IReadingsApiService readingsApiService)
        {
            _readingsApiService = readingsApiService;
        }

        [HttpGet("last-readings")]
        public async Task<Result<List<MeterModel>>> GetAllMetersLastReadingsAsync(long houseId) =>
            await _readingsApiService.GetAllMetersLastReadingsAsync(houseId);

        [HttpGet("readings-history")]
        public async Task<Result<List<ReadingModel>>> GetMeterReadingsHistoryAsync(long meterId) =>
            await _readingsApiService.GetMeterReadingsHistoryAsync(meterId);

        [HttpPost("send-readings")]
        public async Task<Result> SendAllMetersReadingsAsync(long houseId) =>
            await _readingsApiService.SendAllMetersReadingsAsync(houseId);
    }
}
