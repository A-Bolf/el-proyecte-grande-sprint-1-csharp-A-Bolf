using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using SiteRep.Infrastructure.Tests.Common;
using SitRep.Models;
using SitRep.Models.Types;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiteRep.Infrastructure.Tests
{
    public class TicketControllerTests :BaseTest
    {
        [Test]
        public async Task Should_retrives_all_tickest()
        {
            var response = await Client.GetAsync("/api/ticket");
            //Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Ticket>>(responseString);
            var expected = JsonConvert.DeserializeObject<List<Ticket>>(GetDataStructureFromJson("Resources.Ticket.alltickets.json"));
            Assert.That(actual.Count, Is.EqualTo(expected.Count));
        }
        [Test]
        public async Task Should_add_new_ticket()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/ticket");
            postRequest.Content = new StringContent(GetDataStructureFromJson("Resources.Ticket.AddNewTicket.request.json"), Encoding.UTF8, "application/json"); ;

            var response = await Client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Ticket>>(responseString);
            var expected = JsonConvert.DeserializeObject<List<Ticket>>(GetDataStructureFromJson("Resources.Ticket.AddNewTicket.response.json"));
            Assert.That(actual.Count, Is.EqualTo(expected.Count));
        }
        [Test]
        public async Task Should_retrive_the_uptdates()
        {
            var response = await Client.GetAsync("/api/ticket/updates");
            //Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Ticket>>(responseString);
            var expected = JsonConvert.DeserializeObject<List<Ticket>>(GetDataStructureFromJson("Resources.Ticket.RetriveUpdates.response.json"));
            Assert.That(actual.Count, Is.EqualTo(expected.Count));
        }
        public async Task Should_retrive_statuscounts()
        {
          
            var response = await Client.GetAsync("/api/ticket/statuscounts");
            //Assert
            response.EnsureSuccessStatusCode();

           
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<StatusType>(responseString);
            var expected = JsonConvert.DeserializeObject<StatusType>(GetDataStructureFromJson("Resources.Ticket.StatusCounts.response.json"));
            actual.Should().Be(expected);
        }
        [Test]
        public async Task Should_retrive_a_ticket()
        {
            var response = await Client.GetAsync("/api/ticket/1");
            //Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Ticket>(responseString);
            var expected = JsonConvert.DeserializeObject<Ticket>(GetDataStructureFromJson("Resources.Ticket.ByTicketId.response.json"));
             actual.Title.Should().BeEquivalentTo(expected.Title);
        }
        [Test]
        public async Task Should_update_a_ticket()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Put, "/api/ticket/update");
            postRequest.Content = new StringContent(GetDataStructureFromJson("Resources.Ticket.UpdateTicket.request.json"), Encoding.UTF8, "application/json"); ;

            var response = await Client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Ticket>(responseString);
            var expected = JsonConvert.DeserializeObject<Ticket>(GetDataStructureFromJson("Resources.Ticket.UpdateTicket.response.json"));
            actual.Title.Should().BeEquivalentTo(expected.Title); ;
        }
        [Test]
        public async Task Should_delete_a_ticket()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Delete, "/api/ticket/delete/1");
            postRequest.Content = new StringContent(GetDataStructureFromJson("Resources.Ticket.UpdateTicket.request.json"), Encoding.UTF8, "application/json"); ;

            var response = await Client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}