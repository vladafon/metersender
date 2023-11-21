using MetersSender.Api.Models;
using MetersSender.Common;
using MetersSender.Common.Models;
using MetersSender.DataAccess.Repository;

namespace MetersSender.Api.Configuration
{
    ///<inheritdoc/>
    public class ConfigurationApiService : IConfigurationApiService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IRecepientIntegration _recepientIntegration;
        private readonly ISourceIntegration _sourceIntegration;
        public ConfigurationApiService(IDatabaseRepository databaseRepository,
            IRecepientIntegration recepientIntegration,
            ISourceIntegration sourceIntegration) 
        {
            _databaseRepository = databaseRepository;
            _recepientIntegration = recepientIntegration;
            _sourceIntegration = sourceIntegration;
        }

        ///<inheritdoc/>
        public async Task<Result> AddHouseAsync(HouseModel houseModel)
        {
            var recepientIntegrationInfo = await _recepientIntegration.GetServiceInfoAsync();
            var recepientserviceId = await _databaseRepository.AddServiceAsync(recepientIntegrationInfo);

            var sourceIntegrationInfo = await _sourceIntegration.GetServiceInfoAsync();
            var sourceServiceId = await _databaseRepository.AddServiceAsync(sourceIntegrationInfo);

            await _databaseRepository.AddHouseAsync(houseModel, sourceServiceId, recepientserviceId);

            return new Result();
        }

        ///<inheritdoc/>
        public async Task<Result<List<HouseModel>>> GetHousesAsync()
        {
            var result = await _databaseRepository.GetHousesWitMetersAsync();

            return new Result<List<HouseModel>>
            {
                Data = result
            };
        }
    }
}
