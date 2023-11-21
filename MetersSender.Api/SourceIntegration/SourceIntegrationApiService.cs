using MetersSender.Api.Models;
using MetersSender.Common;
using MetersSender.Common.Models;

namespace MetersSender.Api.SourceIntegration
{
    ///<inheritdoc/>
    public class SourceIntegrationApiService : ISourceIntegrationApiService
    {
        private readonly ISourceIntegration _sourceIntegration;
        public SourceIntegrationApiService(ISourceIntegration sourceIntegration)
        {
            _sourceIntegration = sourceIntegration;
        }

        ///<inheritdoc/>
        public async Task<Result<List<MeterModel>>> GetMetersAsync()
        {
            var result = await _sourceIntegration.GetMetersAsync();

            return new Result<List<MeterModel>>
            {
                Data = result
            };
        }
    }
}
