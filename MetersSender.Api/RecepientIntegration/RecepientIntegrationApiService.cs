using MetersSender.Api.Models;
using MetersSender.Common;
using MetersSender.Common.Models;

namespace MetersSender.Api.SourceIntegration
{
    ///<inheritdoc/>
    public class RecepientIntegrationApiService : IRecepientIntegrationApiService
    {
        private readonly IRecepientIntegration _recepientIntegration;
        public RecepientIntegrationApiService(IRecepientIntegration recepientIntegration)
        {
            _recepientIntegration = recepientIntegration;
        }

        ///<inheritdoc/>
        public async Task<Result<List<MeterModel>>> GetMetersAsync()
        {
            var result = await _recepientIntegration.GetMetersAsync();

            return new Result<List<MeterModel>>
            {
                Data = result
            };
        }
    }
}
