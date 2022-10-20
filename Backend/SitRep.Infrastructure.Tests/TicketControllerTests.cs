using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using SitRep.Core.Entities;

namespace SitRep.Infrastructure.Tests
{
    public class TicketControllerTests : BaseTest
    {
        [Test]
        public async Task Should_retrieves_all_tickets()
        {
            var response = await Client.GetAsync("/api/ticket");
            //Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Ticket>>(responseString);
            var expected =
                JsonConvert.DeserializeObject<List<Ticket>>(
                    GetDataStructureFromJson("Resources.Ticket.alltickets.json"));
            Assert.That(actual.Count, Is.EqualTo(expected.Count));
        }

        [Test]
        public async Task Should_add_new_ticket()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/ticket");
            postRequest.Content =
                new StringContent(GetDataStructureFromJson("Resources.Ticket.AddNewTicket.request.json"), Encoding.UTF8,
                    "application/json");

            var response = await Client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Ticket>>(responseString);
            var expected =
                JsonConvert.DeserializeObject<List<Ticket>>(
                    GetDataStructureFromJson("Resources.Ticket.AddNewTicket.response.json"));
            Assert.That(actual.Count, Is.EqualTo(expected.Count));
        }

        [Test]
        public async Task Should_retrieve_the_updates()
        {
            var response = await Client.GetAsync("/api/ticket/updates");
            //Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Ticket>>(responseString);
            var expected =
                JsonConvert.DeserializeObject<List<Ticket>>(
                    GetDataStructureFromJson("Resources.Ticket.RetriveUpdates.response.json"));
            Assert.That(actual.Count, Is.EqualTo(expected.Count));
        }
        

        [Test]
        public async Task Should_retrieve_a_ticket()
        {
            var response = await Client.GetAsync("/api/ticket/1");
            //Assert
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Ticket>(responseString);
            var expected =
                JsonConvert.DeserializeObject<Ticket>(
                    GetDataStructureFromJson("Resources.Ticket.ByTicketId.response.json"));
            actual.Title.Should().BeEquivalentTo(expected.Title);
        }

        [Test]
        public async Task Should_update_a_ticket()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Put, "/api/ticket/update");
            postRequest.Content =
                new StringContent(GetDataStructureFromJson("Resources.Ticket.UpdateTicket.request.json"), Encoding.UTF8,
                    "application/json");

            var response = await Client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Ticket>(responseString);
            var expected =
                JsonConvert.DeserializeObject<Ticket>(
                    GetDataStructureFromJson("Resources.Ticket.UpdateTicket.response.json"));
            actual.Title.Should().BeEquivalentTo(expected.Title);
            
        }

        [Test]
        public async Task Should_delete_a_ticket()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Delete, "/api/ticket/delete/1");
            postRequest.Content =
                new StringContent(GetDataStructureFromJson("Resources.Ticket.UpdateTicket.request.json"), Encoding.UTF8,
                    "application/json");

            var response = await Client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}