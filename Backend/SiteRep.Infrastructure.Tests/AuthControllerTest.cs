using FluentAssertions;
using Newtonsoft.Json;
using SiteRep.Infrastructure.Tests.Common;
using SiteRep.Infrastructure.Tests.Extensions;
using SitRep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SiteRep.Infrastructure.Tests
{
    public class AuthControllerTest : BaseTest
    {
        [Test]
        [TestCase("api/auth/login", "Resources.Auth.user.json")]
        public async Task Should_login_a_user(string route, string pathToTestUser)
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, route);
            postRequest.Content = new StringContent(GetDataStructureFromJson(pathToTestUser), Encoding.UTF8, "application/json"); ;
            
            var response = await Client.SendAsync(postRequest);
           
            response.EnsureSuccessStatusCode();
            
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().NotBeNullOrEmpty();
        }
        [Test]
        public async Task Should_register_a_user()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/register");
            var content = new StringContent(GetDataStructureFromJson("Resources.Auth.register_request.json"), Encoding.UTF8, "application/json");
            postRequest.Content = content;
            // Act
            var response = await Client.SendAsync(postRequest);
            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<RegiterUserDTO>(responseString);
            var expected = JsonConvert.DeserializeObject<RegiterUserDTO>(GetDataStructureFromJson("Resources.Auth.register_expected.json"));
            user.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Should_update_password()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "api/auth/updatepassword");
            var content = new StringContent(GetDataStructureFromJson("Resources.Auth.user.json"), Encoding.UTF8, "application/json");
            postRequest.Content = content;
            // Act
            var response = await Client.SendAsync(postRequest);
            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().NotBeEmpty();
        }
    }
}
