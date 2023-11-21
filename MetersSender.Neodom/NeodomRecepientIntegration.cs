using MetersSender.Common;
using MetersSender.Common.Models;
using MetersSender.Neodom.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MetersSender.Neodom
{
    public class NeodomRecepientIntegration : IRecepientIntegration
    {
        private readonly NeodomConfig _config;

        public NeodomRecepientIntegration()
        {
            var json = File.ReadAllText(Consts.ConfigPath);
            _config = JsonConvert.DeserializeObject<NeodomConfig>(json);
        }
        public async Task<MeterModel> GetMeterByIdAsync(string meterId)
        {
            if (!int.TryParse(meterId, out int meterIdInt))
            {
                throw new ArgumentException($"Значение {meterId} недопустимо для параметра.", nameof(meterId));
            }

            var meters = await GetMetersAsync();

            if (!meters.Any(_ => string.Equals(_.Id, meterId)))
            {
                return null;
            }

            return meters.Where(_ => string.Equals(_.Id, meterId))
                .Select(_ => new MeterModel
                {
                    Id = _.Id,
                    Name = _.Name,
                    ReadingValue = _.ReadingValue
                }).FirstOrDefault();
        }

        public async Task<List<MeterModel>> GetMetersAsync()
        {
            var authCookie = await GetLoginCookieAsync();

            var metersResponse = await new RequestService<MetersResponse>()
                .MakeRequestAsync(_config.ApiUrl, "/", Method.Post, RequestType.Form, authCookie, parameters: new Dictionary<string, string>()
                {
                    { "service[0][name]", "list_meter" }
                });

            var result = new List<MeterModel>();

            if (metersResponse?.ListMeter?.ElectricityMeters != null)
            {
                result.AddRange(metersResponse.ListMeter.ElectricityMeters.Select(_ => new MeterModel
                {
                    Id = _.Id.ToString(),
                    Name = _.Name,
                    ReadingValue = _.LastReading
                }).ToList());
            }

            if (metersResponse?.ListMeter?.WaterMeters != null)
            {
                result.AddRange(metersResponse.ListMeter.WaterMeters.Select(_ => new MeterModel
                {
                    Id = _.Id.ToString(),
                    Name = _.Name,
                    ReadingValue = _.LastReading
                }).ToList());
            }

            return result;
        }

        public Task<ServiceModel> GetServiceInfoAsync() =>
            Task.FromResult(new ServiceModel
            {
                Name = Consts.NeodomName
            });

        public async Task SetMeterReadingAsync(string meterId, decimal readingValue)
        {
            if (!int.TryParse(meterId, out int meterIdInt))
            {
                throw new ArgumentException($"Значение {meterId} недопустимо для параметра.", nameof(meterId));
            }

            var indications = new List<Indication>
            {
                new Indication
                {
                    MeterId = meterIdInt,
                    ReadingValue = readingValue
                }
            };

            var indicationsJson = JsonConvert.SerializeObject(indications);

            var jsonModel = new NeodomModel<IndicationsAttributes>
            {
                Name = "add_indications",
                Attributes = new IndicationsAttributes
                {
                    Indications = indicationsJson
                }
            };

            var authCookie = await GetLoginCookieAsync();

            var requestService = new RequestService<LoginResult>();
            var result = await requestService.MakeRequestAsync(_config.ApiUrl, "/", Method.Post, RequestType.Json, authCookie, jsonString: JsonConvert.SerializeObject(jsonModel));
        }

        private async Task<Cookie> GetLoginCookieAsync()
        {
            var jsonModel = new NeodomModel<LoginAttributes>
            {
                Name = "login",
                Attributes = new LoginAttributes
                {
                    Login = _config.Login,
                    Password = _config.Password
                }
            };

            var requestService = new RequestService<LoginResult>();
            var result = await requestService.MakeLoginAsync(_config.ApiUrl, "/", Method.Post, RequestType.Json, jsonString: JsonConvert.SerializeObject(jsonModel));

            return result;
        }
    }
}
