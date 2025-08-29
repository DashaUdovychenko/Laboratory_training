using ApiAutomation.Business.Builders;
using ApiAutomation.Business.Models;
using ApiAutomation.Core.Client;
using ApiAutomation.Core.Logging;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Allure.NUnit.Attributes;
using Allure.Commons;

namespace ApiAutomation.Tests.ApiTests
{
    [TestFixture]
    [Category("API")]
    [Parallelizable(ParallelScope.All)]
    [AllureSuite("User API Tests")]
    [AllureSubSuite("CRUD Operations")]
    public class UserApiTests
    {
        private UserApiClient _userApiClient = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _userApiClient = new UserApiClient();
        }

        [Test]
        [AllureName("Get list of users")]
        public async Task ValidateListOfUsersCanBeReceived()
        {
            Logger.Info("Test: Validate list of users can be received");

            RestResponse<List<UserModel>> response = await _userApiClient.GetUsersAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code should be 200 OK");
            Assert.That(response.Data, Is.Not.Null.And.Not.Empty, "Response data should not be empty");

            UserModel user = response.Data.First();
            Assert.Multiple(() =>
            {
                Assert.That(user.Id, Is.GreaterThan(0));
                Assert.That(user.Name, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Username, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Email, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Address, Is.Not.Null);
                Assert.That(user.Phone, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Website, Is.Not.Null.And.Not.Empty);
                Assert.That(user.Company, Is.Not.Null);
            });
        }

        [Test]
        [AllureName("Validate response headers")]
        public async Task ValidateResponseHeaderForListOfUsers()
        {
            Logger.Info("Test: Validate response header for list of users");

            RestRequest request = new RestRequest("users", Method.Get);
            RestResponse response = await _userApiClient.client.ExecuteAsync(request);

            Logger.Info($"Status Code: {response.StatusCode}");
            Logger.Info($"Content-Type (via ContentType property): {response.ContentType}");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code should be 200 OK");

            Assert.That(!string.IsNullOrEmpty(response.ContentType), Is.True, "Content-Type should not be null or empty");
            Assert.That(response.ContentType, Does.StartWith("application/json"), "Content-Type should be JSON");
        }

        [Test]
        [AllureName("Validate response body content")]
        public async Task ValidateContentOfResponseBodyForListOfUsers()
        {
            Logger.Info("Test: Validate content of response body for list of users");

            RestResponse<List<UserModel>> response = await _userApiClient.GetUsersAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code should be 200 OK");
            Assert.That(response.Data, Is.Not.Null);

            Assert.That(response.Data.Count, Is.EqualTo(10), "Should receive exactly 10 users");

            IEnumerable<int?> distinctIds = response.Data.Select(u => u.Id).Distinct();
            Assert.That(distinctIds.Count(), Is.EqualTo(10), "Each user should have a unique ID");

            foreach (UserModel user in response.Data)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(user.Name, Is.Not.Null.And.Not.Empty, "User Name should not be empty");
                    Assert.That(user.Username, Is.Not.Null.And.Not.Empty, "User Username should not be empty");
                    Assert.That(user.Company, Is.Not.Null, "User Company should not be null");
                    Assert.That(user.Company!.Name, Is.Not.Null.And.Not.Empty, "Company Name should not be empty");
                });
            }
        }

        [Test]
        [AllureName("Create a new user")]
        public async Task ValidateUserCanBeCreated()
        {
            Logger.Info("Test: Validate user can be created");

            UserModel userToCreate = new UserRequestBuilder()
                .WithName("Test User")
                .WithUsername("testuser123")
                .Build();

            RestResponse<UserModel> response = await _userApiClient.CreateUserAsync(userToCreate);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Status code should be 201 Created");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
            Assert.That(response.Data!.Id, Is.Not.Null, "Created user should have an ID");
        }

        [Test]
        [AllureName("Invalid user endpoint returns 404")]
        public async Task ValidateUserNotFound()
        {
            Logger.Info("Test: Validate user not found on invalid endpoint");

            RestRequest request = new RestRequest("invalidendpoint", Method.Get);
            RestResponse response = await _userApiClient.client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Status code should be 404 Not Found");
        }
    }
}