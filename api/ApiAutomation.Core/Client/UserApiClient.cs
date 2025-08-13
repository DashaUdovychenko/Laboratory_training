using ApiAutomation.Business.Models;
using ApiAutomation.Core.Logging;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAutomation.Core.Client
{
    public class UserApiClient : BaseApiClient
    {
        private const string UsersEndpoint = "users";

        public UserApiClient(string? baseUrl = null) : base(baseUrl)
        {
        }

        public async Task<RestResponse<List<UserModel>>> GetUsersAsync()
        {
            var request = new RestRequest(UsersEndpoint, Method.Get);
            Logger.Info("Sending GET request to /users");

            var response = await client.ExecuteAsync<List<UserModel>>(request);
            Logger.Info($"Received response with status code {(int)response.StatusCode}");

            return response;
        }

        public async Task<RestResponse<UserModel>> CreateUserAsync(UserModel user)
        {
            var request = new RestRequest(UsersEndpoint, Method.Post);
            request.AddJsonBody(user);
            Logger.Info("Sending POST request to /users with user data");

            var response = await client.ExecuteAsync<UserModel>(request);
            Logger.Info($"Received response with status code {(int)response.StatusCode}");

            return response;
        }
    }
}