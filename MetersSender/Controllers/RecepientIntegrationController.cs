using MetersSender.Api.Models;
using MetersSender.Api.SourceIntegration;
using MetersSender.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetersSender.Controllers
{
    [ApiController]
    [Route("api/v1/recepient")]
    public class RecepientIntegrationController : ControllerBase
    {
        private readonly IRecepientIntegrationApiService _recepientIntegrationApiService;
        public RecepientIntegrationController(IRecepientIntegrationApiService recepientIntegrationApiService)
        {
            _recepientIntegrationApiService = recepientIntegrationApiService;
        }

        [HttpGet("recepient-meters")]
        public async Task<Result<List<MeterModel>>> GetMetersAsync() =>
            await _recepientIntegrationApiService.GetMetersAsync();
    }
}
