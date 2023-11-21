using MetersSender.Saures.Models.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MetersSender.Saures
{
    internal class RequestService<T> where T : class
    {
        public async Task<T> MakeRequestAsync(string apiUrl, string relativeUrl, Method method, Dictionary<string, string> parameters)
        {
            var uri = new Uri($"{apiUrl}");
            var client = new RestClient(uri);
            var request = new RestRequest(relativeUrl, method);

            if (method == Method.Get)
            {
                request.AddHeader("User-Agent", "HTTPie/0.9.8");

                foreach (var param in parameters)
                {
                    request.AddQueryParameter(param.Key, param.Value); 
                }
            }
            else if (method == Method.Post)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("User-Agent", "HTTPie/0.9.8");
                request.AddParameter("application/x-www-form-urlencoded", 
                    string.Join("&", parameters.Select(_ => $"{_.Key}={_.Value}").ToList()), ParameterType.RequestBody);
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

            var result = JsonConvert.DeserializeObject<SauresResponse<T>>(response.Content);

            if (result?.Errors?.Count > 0)
            {
                throw new Exception($"Unexpected exception occured during request on {relativeUrl}: {string.Join(";", result.Errors.Select(_ => $"{_.Name}: {_.Message}").ToList())}");
            }

            return result.Data;
        }
    }
}
