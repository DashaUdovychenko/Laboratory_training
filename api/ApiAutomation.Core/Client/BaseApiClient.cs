using RestSharp;
using System;
using System.Threading.Tasks;
using ApiAutomation.Core.Logging;
using ApiAutomation.Core.Configuration;

namespace ApiAutomation.Core.Client
{
    public class BaseApiClient
    {
        public RestClient client;

        public BaseApiClient(string baseUrl = null!)
        {
            var url = baseUrl ?? TestConfig.Instance.BaseUrl;
            var options = new RestClientOptions(url)
            {
                ThrowOnAnyError = false
            };

            client = new RestClient(options);
        }

        public async Task<RestResponse> GetAsync(string resource)
        {
            var request = new RestRequest(resource, Method.Get);
            LogRequest(request);

            var response = await client.ExecuteAsync(request);
            LogResponse(response);

            return response;
        }

        public async Task<RestResponse> PostAsync(string resource, object body)
        {
            var request = new RestRequest(resource, Method.Post);
            request.AddJsonBody(body);
            LogRequest(request);

            var response = await client.ExecuteAsync(request);
            LogResponse(response);
            return response;
        }

        protected void LogRequest(RestRequest request)
        {
            Logger.Info($"Request: {request.Method} {request.Resource}");
        }

        protected void LogResponse(RestResponse response)
        {
            Logger.Info($"Response: {(int)response.StatusCode} {response.StatusDescription}");
            Logger.Info($"Body: {response.Content}");
        }
    }
}