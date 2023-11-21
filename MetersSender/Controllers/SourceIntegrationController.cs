using MetersSender.Api.Models;
using MetersSender.Api.SourceIntegration;
using MetersSender.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetersSender.Controllers
{
    [ApiController]
    [Route("api/v1/source")]
    public class SourceIntegrationController : ControllerBase
    {
        private readonly ISourceIntegrationApiService _sourceIntegrationApiService;
        public SourceIntegrationController(ISourceIntegrationApiService sourceIntegrationApiService)
        {
            _sourceIntegrationApiService = sourceIntegrationApiService;
        }

        [HttpGet(Name = "source-meters")]
        public async Task<Result<List<MeterModel>>> GetMetersAsync() =>
            await _sourceIntegrationApiService.GetMetersAsync();
    }
}
