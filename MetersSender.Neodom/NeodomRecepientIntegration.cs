using MetersSender.Common;
using MetersSender.Common.Models;
using MetersSender.Neodom.Models;
using MetersSender.Neodom.Models.Configuration;
using MetersSender.Neodom.Models.Requests;
using MetersSender.Neodom.Models.Requests.Attributes;
using MetersSender.Neodom.Models.Responses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace MetersSender.Neodom
{
    public class NeodomRecepientIntegration : IRecepientIntegration
    {
        private readonly NeodomConfig _config;
        private readonly ILogger<NeodomRecepientIntegration> _logger;

        public NeodomRecepientIntegration(ILogger<NeodomRecepientIntegration> logger)
        {
            _logger = logger;
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

            var propertyInfo = await new RequestService<MyPropertyResponse>(_logger)
                .MakeRequestAsync(_config.ApiUrl, "/", Method.Post, RequestType.Json, authCookie, jsonString: JsonConvert.SerializeObject(new NeodomRequest<string>
                {
                    Name = "my_property"
                }));

            if (propertyInfo?.Property == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo.Property), "Не найдена собственность, зарегистрированная в аккаунте.");
            }

            var houses = propertyInfo.Property
                .Where(_ => _?.Houses != null)
                .SelectMany(_ => _.Houses)
                .ToList();

            // Запомним, какой дом выбран, чтобы потом вернуть его в качестве основного
            var selectedAccountId = houses
                .Where(_ => _.IsSelectedAsPrimary)
                .Select(_ => _.AccountId)
                .FirstOrDefault();

            var result = new List<MeterModel>();

            foreach (var house in houses)
            {
                await new RequestService<SwitchPropertyResponse>(_logger)
                .MakeRequestAsync(_config.ApiUrl, "/", Method.Post, RequestType.Json, authCookie, jsonString: JsonConvert.SerializeObject(new NeodomRequest<SwitchPropertyAttributes>
                {
                    Name = "switch_property",
                    Attributes = new SwitchPropertyAttributes
                    {
                        AccountId = house.AccountId
                    }
                }));

                var metersResponse = await new RequestService<MetersResponse>(_logger)
                    .MakeRequestAsync(_config.ApiUrl, "/", Method.Post, RequestType.Form, authCookie, parameters: new Dictionary<string, string>()
                    {
                    { "service[0][name]", "list_meter" }
                    });


                if (metersResponse?.ListMeter?.ElectricityMeters != null)
                {
                    result.AddRange(metersResponse.ListMeter.ElectricityMeters.Select(_ => new MeterModel
                    {
                        Id = _.Id.ToString(),
                        Name = $"{house.Address} - {_.Name}",
                        ReadingValue = _.LastReading
                    }).ToList());
                }

                if (metersResponse?.ListMeter?.WaterMeters != null)
                {
                    result.AddRange(metersResponse.ListMeter.WaterMeters.Select(_ => new MeterModel
                    {
                        Id = _.Id.ToString(),
                        Name = $"{house.Address} - {_.Name}",
                        ReadingValue = _.LastReading
                    }).ToList());
                }
            }

            // Меняем собственность обратно на ту, что была
            if (selectedAccountId != null)
            {
                await new RequestService<SwitchPropertyResponse>(_logger)
                    .MakeRequestAsync(_config.ApiUrl, "/", Method.Post, RequestType.Json, authCookie, jsonString: JsonConvert.SerializeObject(new NeodomRequest<SwitchPropertyAttributes>
                    {
                        Name = "switch_property",
                        Attributes = new SwitchPropertyAttributes
                        {
                            AccountId = selectedAccountId
                        }
                    }));
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

            var jsonModel = new NeodomRequest<IndicationsAttributes>
            {
                Name = "add_indications",
                Attributes = new IndicationsAttributes
                {
                    Indications = indicationsJson
                }
            };

            var authCookie = await GetLoginCookieAsync();

            var requestService = new RequestService<SendIndicationsResponse>(_logger);
            await requestService.MakeRequestAsync(_config.ApiUrl, "/", Method.Post, RequestType.Json, authCookie, jsonString: JsonConvert.SerializeObject(jsonModel));
        }

        private async Task<Cookie> GetLoginCookieAsync()
        {
            var jsonModel = new NeodomRequest<LoginAttributes>
            {
                Name = "login",
                Attributes = new LoginAttributes
                {
                    Login = _config.Login,
                    Password = _config.Password
                }
            };

            var requestService = new RequestService<LoginResponse>(_logger);
            var result = await requestService.MakeLoginAsync(_config.ApiUrl, "/", Method.Post, RequestType.Json, jsonString: JsonConvert.SerializeObject(jsonModel));

            return result;
        }
    }
}
