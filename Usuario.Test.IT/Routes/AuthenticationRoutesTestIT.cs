using Newtonsoft.Json;
using System.Net;
using System.Text;
using Usuario.Domain.DTOs;
using Xunit;

namespace Usuario.IntegrationTest
{
    public class AuthenticationRoutesTestIT : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public AuthenticationRoutesTestIT(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task EndpointMustReturnSuccess()
        {
            var client = _factory.CreateClient();
            var login = new Login();
            login.Email = "teste@gmail.com";
            login.Password = "1234567dfsdfds";

            var stringContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/v1/Authentication/Login", stringContent);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task EndpointMustReturnError400DueToNotHavingValidLoginData()
        {
            var client = _factory.CreateClient();
            var login = new Login();
            login.Email = string.Empty;
            login.Password = string.Empty;

            var stringContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/v1/Authentication/Login", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
