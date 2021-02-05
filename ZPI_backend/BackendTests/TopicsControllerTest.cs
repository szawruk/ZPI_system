using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
using ZPI_Database.DataAccess;
using ZPI_Database.Models;

namespace BackendTests
{
    public class TopicsControllerTest
    {
        // Needed in all tests
        private readonly DbContextOptions<ZPIContext> options = new DbContextOptionsBuilder<ZPIContext>().UseInMemoryDatabase(databaseName: "ZPIContext1").Options;
        private readonly ZPIContext testContext;
        private readonly TopicsController topicContr;

        public TopicsControllerTest()
        {
            testContext = new ZPIContext(options);
            testContext.Database.EnsureDeleted();
            topicContr = new TopicsController(testContext);
        }

        [Fact]
        public void AddTest()
        {
            var topic1 = new Topic()
            {
                Name = "Nazwa testowa",
                Description = "Opis testowy",
            };
            var topic2 = new Topic()
            {
                Name = "Nazwa testowa2",
                Description = "Opis testowy",
            };

            var res1 = ((ObjectResult)topicContr.PostTopic(topic1).Result.Result).StatusCode;
            var res2 = ((ObjectResult)topicContr.PostTopic(topic2).Result.Result).StatusCode;

            Assert.True(res1==201 && res2==201);
        }

        [Fact]
        public void AddDuplicateTestAsync()
        {
            var topic1 = new Topic()
            {
                Name = "Nazwa testowa",
                Description = "Opis testowy",
            };
            var topic2 = new Topic()
            {
                Name = "Nazwa testowa",
                Description = "Opis testowy",
            };
            _ = topicContr.PostTopic(topic1).Result;
            var res2 = ((ObjectResult)topicContr.PostTopic(topic2).Result.Result).StatusCode;

            Assert.Equal(400, res2);
        }
    }
}
