using ApiAutomation.Business.Models;
using ApiAutomation.Core.Logging;
using ApiAutomation.Core.Configuration;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAutomation.Core.Client
{
    public class UserApiClient : BaseApiClient
    {
        private const string UsersEndpoint = "users";

        public UserApiClient(string? baseUrl = null) : base(baseUrl ?? TestConfig.Instance.BaseUrl)
        {
        }
        public async Task<RestResponse<List<UserModel>>> GetUsersAsync()
        {
            RestRequest request = new RestRequest(UsersEndpoint, Method.Get);
            Logger.Info("Sending GET request to /users");

            RestResponse<List<UserModel>> response = await client.ExecuteAsync<List<UserModel>>(request);
            Logger.Info($"Received response with status code {(int)response.StatusCode}");

            return response;
        }

        public async Task<RestResponse<UserModel>> CreateUserAsync(UserModel user)
        {
            RestRequest request = new RestRequest(UsersEndpoint, Method.Post);
            request.AddJsonBody(user);
            Logger.Info("Sending POST request to /users with user data");

            RestResponse<UserModel> response = await client.ExecuteAsync<UserModel>(request);
            Logger.Info($"Received response with status code {(int)response.StatusCode}");

            return response;
        }
    }
}