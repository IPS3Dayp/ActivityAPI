using Microsoft.VisualStudio.TestTools.UnitTesting;
using DayPlannerAPI.Controllers;
using DayPlannerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DayPlannerAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace DayPlannerAPI.Controllers.Tests
{
    [TestClass()]
    public class PlannedActivitiesControllerTests
    {
        private PlannedActivitiesController _controller;
        private ActivityDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ActivityDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ActivityDbContext(options);
            _controller = new PlannedActivitiesController(_context);
        }

        [TestMethod()]
        public async Task GetPlannedActivitiesByCurrentDateTimeTest()
        {
            // Arrange
            var today = DateTime.Today;
            var plannedActivity1 = new PlannedActivity { Id = 1, ActivityName = "Activity 1", StartTime = today };
            var plannedActivity2 = new PlannedActivity { Id = 2, ActivityName = "Activity 2", StartTime = today.AddDays(1) };

            _context.PlannedActivities.Add(plannedActivity1);
            _context.PlannedActivities.Add(plannedActivity2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetPlannedActivitiesByCurrentDateTime();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<PlannedActivity>));
            Assert.AreEqual(1, result.Value.Count());
            Assert.AreEqual("Activity 1", result.Value.First().ActivityName);
        }

        [TestMethod()]
        public async Task GetPlannedActivitiesTest()
        {
            // Arrange
            _context.PlannedActivities.Add(new PlannedActivity { Id = 3, ActivityName = "Activity 1" });
            _context.PlannedActivities.Add(new PlannedActivity { Id = 4, ActivityName = "Activity 2" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetPlannedActivities();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<PlannedActivity>));
            Assert.AreEqual(5, result.Value.Count());
        }

        [TestMethod()]
        public async Task GetPlannedActivityTest()
        {
            // Arrange
            _context.PlannedActivities.Add(new PlannedActivity { Id = 5, ActivityName = "Activity 1" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetPlannedActivity(5);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(PlannedActivity));
            Assert.AreEqual("Activity 1", result.Value.ActivityName);
        }

        [TestMethod()]
        public async Task PostPlannedActivityTest()
        {
            // Arrange
            var plannedActivity = new PlannedActivity { Id = 6, ActivityName = "New Activity" };

            // Act
            var result = await _controller.PostPlannedActivity(plannedActivity);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            var createdResult = (CreatedAtActionResult)result.Result;
            Assert.AreEqual("GetPlannedActivity", createdResult.ActionName);
            Assert.AreEqual(6, createdResult.RouteValues["id"]);
            Assert.AreEqual("New Activity", ((PlannedActivity)createdResult.Value).ActivityName);
        }

        [TestMethod()]
        public async Task PutPlannedActivityTest()
        {
            // Arrange
            var plannedActivity = new PlannedActivity { Id = 7, ActivityName = "Updated Activity" };
            _context.PlannedActivities.Add(plannedActivity);
            await _context.SaveChangesAsync();

            // Act
            plannedActivity.ActivityName = "Modified Activity";
            var result = await _controller.PutPlannedActivity(7, plannedActivity);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod()]
        public async Task DeletePlannedActivityTest()
        {
            // Arrange
            var plannedActivity = new PlannedActivity { Id = 8, ActivityName = "Activity 8" };
            _context.PlannedActivities.Add(plannedActivity);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeletePlannedActivity(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}