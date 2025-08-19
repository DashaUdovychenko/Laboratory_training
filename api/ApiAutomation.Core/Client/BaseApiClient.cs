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
            string url = baseUrl ?? TestConfig.Instance.BaseUrl;
            RestClientOptions options = new RestClientOptions(url)
            {
                ThrowOnAnyError = false
            };

            client = new RestClient(options);
        }

        public async Task<RestResponse> GetAsync(string resource)
        {
            RestRequest request = new RestRequest(resource, Method.Get);
            LogRequest(request);

            RestResponse response = await client.ExecuteAsync(request);
            LogResponse(response);

            return response;
        }

        public async Task<RestResponse> PostAsync(string resource, object body)
        {
            RestRequest request = new RestRequest(resource, Method.Post);
            request.AddJsonBody(body);
            LogRequest(request);

            RestResponse response = await client.ExecuteAsync(request);
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