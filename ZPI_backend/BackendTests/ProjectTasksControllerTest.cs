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
    public class ProjectTasksControllerTest
    {
        // Needed in all tests
        private readonly DbContextOptions<ZPIContext> options = new DbContextOptionsBuilder<ZPIContext>().UseInMemoryDatabase(databaseName: "ZPIContext2").Options;
        private readonly ZPIContext testContext;
        private readonly ProjectTasksController taskController;

        public ProjectTasksControllerTest()
        {
            testContext = new ZPIContext(options);
            testContext.Database.EnsureDeleted();
            taskController = new ProjectTasksController(testContext);
            taskController.ControllerContext.HttpContext = new DefaultHttpContext();
            InitUsers();
        }
        ~ProjectTasksControllerTest()
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
                Name = "New team"
            };

            testContext.Teams.Add(team);
            team.Students.Add(user2);
            team.Students.Add(user3);

            testContext.SaveChanges();
        }

        [Fact]
        public void AddTaskToYourself()
        {
            // Token of User1 who doesn't have a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            var taskUser = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "Task",
                    Description = "Opis testowy",
                    Deadline = DateTime.Now,

                },
                StudentId = 2
            };
            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(201, res);
        }

        [Fact]
        public void AddTaskToTeammate()
        {
            // Token of User1 who doesn't have a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            var taskUser = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "Task",
                    Description = "Opis testowy",
                    Deadline = DateTime.Now,

                },
                StudentId = 3
            };
            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(201, res);
        }

        [Fact]
        public void AddTaskUserNotInTeam()
        {
            // Token of User1 who doesn't have a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            var taskUser = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "Task",
                    Description = "Opis testowy",
                    Deadline = DateTime.Now,

                },
                StudentId = 1
            };
            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }

        [Fact]
        public void AddTaskWithoutTeam()
        {
            // Token of User1 who doesn't have a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token1";
            var taskUser = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "Task 1",
                    Description = "Opis testowy",
                    Deadline = DateTime.Now,
                },
                StudentId = 1
            };

            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }

        [Fact]
        public void GetTaskList()
        {
            // Token of User1 who doesn't have a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            var taskUser1 = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "Task 1",
                    Description = "Opis testowy1",
                    Deadline = DateTime.Now,
                },
                StudentId = 3
            };
            var taskUser2 = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "Task 2",
                    Description = "Opis testowy2",
                    Deadline = DateTime.Now,

                },
                StudentId = 2
            };
            var taskUser3 = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "Task 3",
                    Description = "Opis testowy3",
                    Deadline = DateTime.Now,

                },
                StudentId = 3
            };
            _ = taskController.PostProjectTask(taskUser1).Result;
            _ = taskController.PostProjectTask(taskUser2).Result;
            var xd = taskController.PostProjectTask(taskUser3).Result;
            var res = (taskController.GetTasks().Result).Value;

            Assert.Equal(3, res.Count());
        }
    }
}
