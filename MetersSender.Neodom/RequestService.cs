using MetersSender.Neodom.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MetersSender.Neodom
{
    internal class RequestService<T> where T : class
    {
        public async Task<T> MakeRequestAsync(string apiUrl, string relativeUrl, Method method, RequestType requestType, Cookie authCookie, Dictionary<string, string> parameters = null, string jsonString = null)
        {
            var contentType = requestType switch
            {
                RequestType.Json => "application/json",
                RequestType.Form => "application/x-www-form-urlencoded",
                _ => throw new ArgumentException($"Задано неверное значение '{requestType}' параметра.", nameof(requestType))
            };

            var uri = new Uri($"{apiUrl}");
            var client = new RestClient(uri);
            var request = new RestRequest(relativeUrl, method);

            request.AddCookie(authCookie.Name, authCookie.Value, authCookie.Path, authCookie.Domain);

            request.AddHeader("Content-Type", contentType);

            if (method == Method.Post)
            {
                request.AddHeader("Content-Type", contentType);
                request.AddHeader("User-Agent", @"""Google Chrome"";v=""119"", ""Chromium"";v=""119"", ""Not?A_Brand"";v=""24""");
                if (requestType == RequestType.Form)
                {
                    if (parameters == null)
                    {
                        throw new ArgumentException($"Не задано значение парметра.", nameof(parameters));
                    }

                    request.AddParameter("application/x-www-form-urlencoded",
                        string.Join("&", parameters.Select(_ => $"{_.Key}={_.Value}").ToList()), ParameterType.RequestBody);
                }
                else if (requestType == RequestType.Json)
                {
                    if (jsonString == null)
                    {
                        throw new ArgumentException($"Не задано значение парметра.", nameof(jsonString));
                    }

                    request.AddJsonBody(jsonString, false);
                }
            }
            else
            {
                throw new NotImplementedException($"Unsupported method {method}");
            }

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw response.ErrorException ?? new Exception($"Unexpected exception occured during request on {relativeUrl}");
            }
            
            var result = JsonConvert.DeserializeObject<T>(response.Content);

            return result;
        }

        public async Task<Cookie> MakeLoginAsync(string apiUrl, string relativeUrl, Method method, RequestType requestType, Dictionary<string, string> parameters = null, string jsonString = null)
        {
            var contentType = requestType switch
            {
                RequestType.Json => "application/json",
                RequestType.Form => "application/x-www-form-urlencoded",
                _ => throw new ArgumentException($"Задано неверное значение '{requestType}' параметра.", nameof(requestType))
            };

            var uri = new Uri($"{apiUrl}");
            var client = new RestClient(uri);
            var request = new RestRequest(relativeUrl, method);

            request.AddHeader("Content-Type", contentType);

            if (method == Method.Post)
            {
                request.AddHeader("Content-Type", contentType);
                request.AddHeader("User-Agent", @"""Google Chrome"";v=""119"", ""Chromium"";v=""119"", ""Not?A_Brand"";v=""24""");
                if (requestType == RequestType.Form)
                {
                    if (parameters == null)
                    {
                        throw new ArgumentException($"Не задано значение парметра.", nameof(parameters));
                    }

                    request.AddParameter("application/x-www-form-urlencoded",
                        string.Join("&", parameters.Select(_ => $"{_.Key}={_.Value}").ToList()), ParameterType.RequestBody);
                }
                else if (requestType == RequestType.Json)
                {
                    if (jsonString == null)
                    {
                        throw new ArgumentException($"Не задано значение парметра.", nameof(jsonString));
                    }

                    request.AddJsonBody(jsonString, false);
                }
            }
            else
            {
                throw new NotImplementedException($"Unsupported method {method}");
            }

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw response.ErrorException ?? new Exception($"Unexpected exception occured during request on {relativeUrl}");
            }

            var requestResult = JsonConvert.DeserializeObject<LoginResult>(response.Content);

            if (requestResult.Error != null)
            {
                throw response.ErrorException ?? new Exception($"Unexpected exception occured during request on {relativeUrl}: {requestResult.Code} - {requestResult.Error}");
            }

            var cookie = response.Cookies?.Where(_ => _.Name.Contains("id", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (cookie == null)
            {
                throw new Exception("Отсутствуют cookie авторизации.");
            }

            return new Cookie
            {
                Name = cookie.Name,
                Value = cookie.Value,
                Domain = cookie.Domain,
                Path = cookie.Path
            };
        }
    }
}
