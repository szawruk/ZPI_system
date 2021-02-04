using Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using ZPI_Database.DataAccess;
using ZPI_Database.Models;

namespace BackendTests
{
    public class UnitTest1
    {
        DbContextOptions<ZPIContext> options = new DbContextOptionsBuilder<ZPIContext>().UseInMemoryDatabase(databaseName: "ZPIContext").Options;

        [Fact]
        public void Test1()
        {
            var testContext = new ZPIContext(options);
            var topicContr = new TopicsController(testContext);


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
            var res1 = ((ObjectResult)topicContr.PostTopic(topic1).Result.Result).StatusCode;
            var res2 = ((ObjectResult)topicContr.PostTopic(topic2).Result.Result).StatusCode;

            Assert.Equal(400,res2);

        }
    }
}
