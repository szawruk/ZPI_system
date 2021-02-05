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
    [Collection("ControllerTests")]
    public class ProjectTasksControllerTest
    {
        // Needed in all tests
        private readonly DbContextOptions<ZPIContext> options = new DbContextOptionsBuilder<ZPIContext>().UseInMemoryDatabase(databaseName: "ZPIContext").Options;
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
        public void AddTaskToYourselfTest()
        {
            // Token of User2 who has a team
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
        public void AddTaskToTeammateTest()
        {
            // Token of User2 who has team
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
        public void AddTaskUserNotInTeamTest()
        {
            // Token of User2 who has a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            var taskUser = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "Task",
                    Description = "Opis testowy",
                    Deadline = DateTime.Now
                },
                StudentId = 1
            };
            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }

        [Fact]
        public void AddTaskWithoutTeamTest()
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
        public void AddTaskNullTest()
        {
            // Token of User2 who has a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            TaskUser taskUser = null;

            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }

        [Fact]
        public void AddTaskTaskNullTest()
        {
            // Token of User2 who has a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            var taskUser = new TaskUser()
            {
                ProjectTask = null,
                StudentId = 1
            };

            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }

        [Fact]
        public void AddTaskUserNullTest()
        {
            // Token of User2 who has a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            var taskUser = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "Task 1",
                    Description = "Opis testowy1",
                    Deadline = DateTime.Now,
                }
            };

            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }

        [Fact]
        public void AddTaskNameEmptyTest()
        {
            // Token of User2 who has a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            var taskUser = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = "",
                    Description = "Opis testowy1",
                    Deadline = DateTime.Now,
                },
                StudentId=2
            };

            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }

        [Fact]
        public void AddTaskNameNullTest()
        {
            // Token of User2 who has a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token2";
            var taskUser = new TaskUser()
            {
                ProjectTask = new ProjectTask()
                {
                    Name = null,
                    Description = "Opis testowy1",
                    Deadline = DateTime.Now,
                },
                StudentId = 2
            };

            var res = ((ObjectResult)(taskController.PostProjectTask(taskUser).Result.Result)).StatusCode;

            Assert.Equal(400, res);
        }

        [Fact]
        public void GetTaskList()
        {
            // Token of User2 who has a team
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
            _ = taskController.PostProjectTask(taskUser3).Result;
            var res = (taskController.GetTasks().Result).Value;

            Assert.Equal(3, res.Count());
        }

        [Fact]
        public void GetTaskListNoTeam()
        {
            // Token of User1 who don't have a team
            taskController.HttpContext.Request.Headers["Authorization"] = "Bearer token1";

            var res = ((ObjectResult)(taskController.GetTasks().Result.Result)).StatusCode;

            var 

            Assert.Equal(400, res);
        }
    }
}
