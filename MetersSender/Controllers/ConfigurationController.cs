using MetersSender.Api.Configuration;
using MetersSender.Api.Models;
using MetersSender.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetersSender.Controllers
{
    [ApiController]
    [Route("api/v1/configuration")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationApiService _configurationApiService;
        public ConfigurationController(IConfigurationApiService configurationApiService) 
        { 
            _configurationApiService = configurationApiService;
        }

        [HttpGet("houses")]
        public async Task<Result<List<HouseModel>>> GetHousesAsync() =>
            await _configurationApiService.GetHousesAsync();

        [HttpPost("house")]
        public async Task<Result> AddHouseAsync(HouseModel houseModel) =>
            await _configurationApiService.AddHouseAsync(houseModel);
    }
}
