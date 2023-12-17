using MetersSender.Common;
using MetersSender.Common.Models;
using MetersSender.Saures.Models;
using MetersSender.Saures.Models.Configuration;
using MetersSender.Saures.Models.Requests;
using Newtonsoft.Json;
using RestSharp;

namespace MetersSender.Saures
{
    /// <summary>
    ///     Интеграция с сервисом Saures.
    /// </summary>
    public class SauresSourceIntegration : ISourceIntegration
    {
        private readonly SauresConfig _config;

        public SauresSourceIntegration() 
        {
            var json = File.ReadAllText(Consts.ConfigPath);
            _config = JsonConvert.DeserializeObject<SauresConfig>(json);
        }

        ///<inheritdoc/>
        public async Task<MeterModel> GetMeterByIdAsync(string meterId)
        {
            if (!int.TryParse(meterId, out int meterIdInt))
            {
                throw new ArgumentException($"Значение {meterId} недопустимо для параметра.", nameof(meterId));
            }

            var meters = await GetMetersAsync();

            if (!meters.Any(_ => string.Equals(_.SourceMeterId, meterId)))
            {
                return null;
            }

            return meters.Where(_ => string.Equals(_.SourceMeterId, meterId))
                .Select(_ => new MeterModel
            {
                SourceMeterId = _.SourceMeterId,
                Name = _.Name,
                ReadingValue = _.ReadingValue
            }).FirstOrDefault();
        }

        ///<inheritdoc/>
        public async Task<List<MeterModel>> GetMetersAsync()
        {
            var sid = await GetLoginSidAsync();

            var userObjects = await new RequestService<UserObjectsRequest>()
                .MakeRequestAsync(_config.ApiUrl, "user/objects", Method.Get, new Dictionary<string, string>()
                {
                    { "sid", sid.ToString() }
                });

            if (userObjects?.Objects == null)
            {
                return new List<MeterModel>();
            }

            var allObjectMeters = new List<Meter>();

            foreach (var obj in userObjects.Objects)
            {
                var objectMeters = await new RequestService<ObjectMetersRequest>()
                    .MakeRequestAsync(_config.ApiUrl, "object/meters", Method.Get, new Dictionary<string, string>()
                    {
                        { "sid", sid.ToString() },
                        { "id", obj.Id.ToString() }
                    });

                if (objectMeters?.Sensors != null)
                {
                    var sensors = objectMeters.Sensors.Where(_ => _?.Meters != null).SelectMany(_ => _.Meters).ToList();

                    foreach(var sensor in sensors)
                    {
                        sensor.Name = $"{obj.ApartmentAddress} {obj.ApartmentNumber} - {sensor.Name}";
                    }

                    allObjectMeters.AddRange(sensors);
                }
            }

            return allObjectMeters.Select(_ => new MeterModel
            {
                SourceMeterId = _.Id.ToString(),
                Name = $"{_.Name} ({_.Type?.Name}) #{_.SerialNumber}",
                ReadingValue = _.Readings.FirstOrDefault()
            }).ToList();
        }

        ///<inheritdoc/>
        public Task<ServiceModel> GetServiceInfoAsync() =>
            Task.FromResult(new ServiceModel
            {
                Name = Consts.SauresName
            });

        private async Task<Guid> GetLoginSidAsync()
        {
            var requestService = new RequestService<LoginRequest>();
            var result = await requestService.MakeRequestAsync(_config.ApiUrl, "login", Method.Post, new Dictionary<string, string>()
            {
                { "email", _config.Email },
                { "password", _config.Password }
            });

            return result.Sid;
        }
    }
}
