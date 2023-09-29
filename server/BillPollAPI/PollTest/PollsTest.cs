using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using WebApplication1;
using WebApplication1.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;

namespace PollTest
{
    [TestClass]
    public class PollsTest
    {
        private Mock<ILogger<PollsController>>? _loggerMock;
        private PollsController? _controller;
        private PollContext? _context;

        private void AssertStatusCode(int expected, IActionResult? actionResult)
        {
            Assert.AreEqual(expected, (actionResult as IStatusCodeActionResult)?.StatusCode);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _loggerMock = new Mock<ILogger<PollsController>>();

            // Need to assign a random name to the database name
            // Otherwise the database won't clean up properly (...for some reason)
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<PollContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase" + dbName)
            .Options;
            _context = new PollContext(options);
            _controller = new PollsController(_loggerMock.Object, _context);
        }

        [TestMethod]
        public void Test_GetAllPolls()
        {
            Assert.IsNotNull(_context);
            Assert.IsNotNull(_controller);

            // Arrange
            var existingPoll = new WebApplication1.Models.Poll(
               1,
               "Test Poll",
               new WebApplication1.Models.Option[] {
                new WebApplication1.Models.Option { Id = 1, Name = "Option 1", Votes = 1 },
                new WebApplication1.Models.Option { Id = 2, Name = "Option 2", Votes = 1 }
               });
            _context.Add(existingPoll);
            _context.SaveChanges();

            // Act
            var result = _controller.Get().Result as ObjectResult;

            WebApplication1.API.Poll[] expected = { new WebApplication1.API.Poll(
                1,
                "Test Poll",
                new WebApplication1.API.Option[] {
                    new WebApplication1.API.Option { ID = 1, Name = "Option 1", Votes = 1 },
                    new WebApplication1.API.Option { ID = 2, Name = "Option 2", Votes = 1 }
                })
            };

            // Assert
            AssertStatusCode(200, result);

            var resultJson = JsonConvert.SerializeObject(result?.Value);
            var expectedJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
        }
        [TestMethod]
        public void Test_GetPoll()
        {
            Assert.IsNotNull(_context);
            Assert.IsNotNull(_controller);

            // Arrange
            var existingPoll = new WebApplication1.Models.Poll(
               1,
               "Test Poll",
               new WebApplication1.Models.Option[] {
                new WebApplication1.Models.Option { Id = 1, Name = "Option 1", Votes = 1 },
                new WebApplication1.Models.Option { Id = 2, Name = "Option 2", Votes = 1 }
               });
            _context.Add(existingPoll);
            _context.SaveChanges();


            var result = _controller.Get(1).Result as ObjectResult;

            WebApplication1.API.Poll expected = new WebApplication1.API.Poll(
                1,
                "Test Poll",
                new WebApplication1.API.Option[] {
                    new WebApplication1.API.Option { ID = 1, Name = "Option 1", Votes = 1 },
                    new WebApplication1.API.Option { ID = 2, Name = "Option 2", Votes = 1 }
                });

            AssertStatusCode(200, result);

            var resultJson = JsonConvert.SerializeObject(result?.Value);
            var expectedJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
        }
        [TestMethod]
        public async Task Test_DeletePoll()
        {
            Assert.IsNotNull(_context);
            Assert.IsNotNull(_controller);

            // Arrange
            var existingPoll = new WebApplication1.Models.Poll(
               1,
               "Test Poll",
               new WebApplication1.Models.Option[] {
                new WebApplication1.Models.Option { Id = 1, Name = "Option 1", Votes = 1 },
                new WebApplication1.Models.Option { Id = 2, Name = "Option 2", Votes = 1 }
               });
            _context.Add(existingPoll);
            _context.SaveChanges();

            var result = (await _controller.Delete(1)).Result;

            AssertStatusCode(204, result);

            var resultAfterDelete = _context.Polls.SingleOrDefault(p => p.Id == 1);
            Assert.IsNull(resultAfterDelete);
        }

        [TestMethod]
        public async Task Test_CreatePoll()
        {
            Assert.IsNotNull(_context);
            Assert.IsNotNull(_controller);

            // Arrange
            var toCreate = new WebApplication1.API.PollCreate(
               "Posted Poll",
               new WebApplication1.API.OptionCreate[] {
                new WebApplication1.API.OptionCreate { Name = "Option 1" },
                new WebApplication1.API.OptionCreate { Name = "Option 2" }
               });

            // Act
            var result = (await _controller.CreatePoll(toCreate)).Result as ObjectResult;


            // Assert
            WebApplication1.API.Poll expected = new WebApplication1.API.Poll(
               1,
               "Posted Poll",
                new WebApplication1.API.Option[] {
                    new WebApplication1.API.Option { ID = 1, Name = "Option 1" },
                    new WebApplication1.API.Option { ID = 2, Name = "Option 2" }
                }
            );
            var resultJson = JsonConvert.SerializeObject(result?.Value);
            var expectedJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
            AssertStatusCode(201, result);
        }

        [TestMethod]
        public async Task Test_Vote()
        {

            Assert.IsNotNull(_context);
            Assert.IsNotNull(_controller);

            //Arrange
            var existingPoll = new WebApplication1.Models.Poll(
               1,
               "Test Poll",
               new WebApplication1.Models.Option[] {
                new WebApplication1.Models.Option { Id = 1, Name = "Option 1", Votes = 1 },
                new WebApplication1.Models.Option { Id = 2, Name = "Option 2", Votes = 1 }
               });
            _context.Add(existingPoll);
            _context.SaveChanges();

            // Act
            var result = (await _controller.Vote(1, 1)).Result as ObjectResult;

            //Assert
            WebApplication1.API.Poll expected = new WebApplication1.API.Poll(
               1,
               "Test Poll",
                new WebApplication1.API.Option[] {
                                new WebApplication1.API.Option { ID = 1, Name = "Option 1", Votes = 2 },
                                new WebApplication1.API.Option { ID = 2, Name = "Option 2", Votes = 1 }
                }
            );
            var resultJson = JsonConvert.SerializeObject(result?.Value);
            var expectedJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
            AssertStatusCode(200, result);
        }
    }
}