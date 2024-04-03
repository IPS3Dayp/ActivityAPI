using Xunit;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using DayPlannerAPI.Models;
using System.Collections.Generic;

namespace DayPlannerE2ETests
{
    public class PlannedActivitiesControllerE2ETests
    {
        private readonly RestClient _client;

        public PlannedActivitiesControllerE2ETests()
        {
            _client = new RestClient("https://localhost:5001/api/PlannedActivities"); // Update with your API URL
        }

        [Fact]
        public void TestGetAllPlannedActivities()
        {
            var request = new RestRequest(Method.GET);
            var response = _client.Execute(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var plannedActivities = JsonConvert.DeserializeObject<List<PlannedActivity>>(response.Content);

            Assert.NotNull(plannedActivities);
            Assert.NotEmpty(plannedActivities);
        }

        [Fact]
        public void TestGetPlannedActivityById()
        {
            var id = 1; // Replace with a valid planned activity ID
            var request = new RestRequest($"/{id}", Method.GET);
            var response = _client.Execute(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var plannedActivity = JsonConvert.DeserializeObject<PlannedActivity>(response.Content);

            Assert.NotNull(plannedActivity);
            Assert.Equal(id, plannedActivity.Id);
        }

        [Fact]
        public void TestGetPlannedActivitiesByCurrentDateTime()
        {
            var request = new RestRequest("/currentdatetime", Method.GET);
            var response = _client.Execute(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var plannedActivities = JsonConvert.DeserializeObject<List<PlannedActivity>>(response.Content);

            Assert.NotNull(plannedActivities);
            // Add assertions based on the current date and planned activities
        }

        [Fact]
        public void TestPostPlannedActivity()
        {
            var newPlannedActivity = new PlannedActivity
            {
                // Initialize with necessary properties
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(newPlannedActivity);

            var response = _client.Execute(request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdPlannedActivity = JsonConvert.DeserializeObject<PlannedActivity>(response.Content);

            Assert.NotNull(createdPlannedActivity);
            // Add assertions based on the created planned activity
        }

        [Fact]
        public void TestPutPlannedActivity()
        {
            var id = 1; // Replace with a valid planned activity ID
            var updatedPlannedActivity = new PlannedActivity
            {
                Id = id,
                // Update with necessary properties
            };

            var request = new RestRequest($"/{id}", Method.PUT);
            request.AddJsonBody(updatedPlannedActivity);

            var response = _client.Execute(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Verify the updated planned activity
        }

        [Fact]
        public void TestDeletePlannedActivity()
        {
            var id = 1; // Replace with a valid planned activity ID
            var request = new RestRequest($"/{id}", Method.DELETE);

            var response = _client.Execute(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Verify the planned activity is deleted
        }
    }
}

