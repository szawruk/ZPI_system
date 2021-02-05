using Backend.Controllers;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZPI_Database.DataAccess;
using ZPI_Database.Models;

namespace BackendTests
{
    public class TeamsControllerTest
    {
        private readonly DbContextOptions<ZPIContext> options = new DbContextOptionsBuilder<ZPIContext>().UseInMemoryDatabase(databaseName: "ZPIContext3").Options;
        private readonly ZPIContext testContext;
        private readonly TeamsController teamController;

        public TeamsControllerTest()
        {
            testContext = new ZPIContext(options);
            testContext.Database.EnsureDeleted();
            teamController = new TeamsController(testContext);
            teamController.ControllerContext.HttpContext = new DefaultHttpContext();
            InitUsers();
        }

        ~TeamsControllerTest()
        {
            testContext.Database.EnsureDeleted();
        }

        private void InitUsers()
        {
            // User without team
            var user1 = new User()
            {
                Id = 1,
                Name = "Adam",
                Surname = "Sierakowski",
                Email = "adam@sierakowski.com",
                Password = "admin123",
                AccountType = AccountType.stud.ToString(),
                Token = "token1"
            };
            var user2 = new User()
            {
                Id = 2,
                Name = "Adam",
                Surname = "Teamowy",
                Email = "adamTeam@sierakowski.com",
                Password = "admin123",
                AccountType = AccountType.stud.ToString(),
                Token = "token2"
            };
            var user3 = new User()
            {
                Id = 3,
                Name = "Adam2",
                Surname = "Teamowy2",
                Email = "adamTeam2@sierakowski.com",
                Password = "admin123",
                AccountType = AccountType.stud.ToString(),
                Token = "token3"
            };
            testContext.Users.Add(user1);
            testContext.Users.Add(user2);
            testContext.Users.Add(user3);
            var team = new Team()
            {
                Id=1,
                Name = "New team 1"
            };

            testContext.Teams.Add(team);
            team.Students.Add(user2);
            team.Students.Add(user3);

            testContext.SaveChanges();
        }


        [Fact]
        public void CreatingTeam()
        {
            // Token of User1 who doesn't have a team
            teamController.HttpContext.Request.Headers["Authorization"] = "Bearer token1";

            var tT = new TeamTopic()
            {
                Team = new Team()
                {
                    Id = 2,
                    Name = "New Team 2"
                }
            };
            var res = ((ObjectResult)(teamController.PostTeam(tT).Result.Result)).StatusCode; ;

            Assert.Equal(201, res);
        }

        [Fact]
        public void CreatingTeamTopicExist()
        {
            // Token of User1 who doesn't have a team
            teamController.HttpContext.Request.Headers["Authorization"] = "Bearer token1";

            var topic = new Topic()
            {
                Id=1,
                Name = "Nazwa testowa",
                Description = "Opis testowy",
            };

            testContext.Topics.Add(topic);
            var tT = new TeamTopic()
            {
                Team = new Team()
                {
                    Id = 2,
                    Name = "New Team 2"
                },
                TopicId=1
                
            };
            var res = ((ObjectResult)(teamController.PostTeam(tT).Result.Result)).StatusCode;

            Assert.Equal(201, res);
        }

        [Fact]
        public void CreatingTeamTopicNoExist()
        {
            // Token of User1 who doesn't have a team
            teamController.HttpContext.Request.Headers["Authorization"] = "Bearer token1";

            var topic = new Topic()
            {
                Id = 1,
                Name = "Nazwa testowa",
                Description = "Opis testowy",
            };

            testContext.Topics.Add(topic);
            var tT = new TeamTopic()
            {
                Team = new Team()
                {
                    Id = 2,
                    Name = "New Team 2"
                },
                TopicId = 2

            };
            var res = ((ObjectResult)(teamController.PostTeam(tT).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }

        [Fact]
        public void CreatingTeamAgain()
        {
            // Token of User1 who has a team
            teamController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";

            var tT = new TeamTopic()
            {
                Team = new Team()
                {
                    Id = 2,
                    Name = "New Team 2"
                }
            };
            var res = ((ObjectResult)(teamController.PostTeam(tT).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }
    }
}
