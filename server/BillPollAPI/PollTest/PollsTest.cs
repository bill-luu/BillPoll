using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using WebApplication1;
using WebApplication1.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PollTest
{
    [TestClass]
    public class PollsTest
    {
        private Mock<ILogger<PollsController>>? _loggerMock;
        private PollsController? _controller;
        private PollContext? _context;

        [TestInitialize]
        public void TestInitialize()
        {
            _loggerMock = new Mock<ILogger<PollsController>>();

            // Need to assign a random name to the database name
            // Otherwise the database won't clean up properly (...for some reason)
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<PollContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase"+ dbName)
            .Options;
            _context = new PollContext(options);
            _controller = new PollsController(_loggerMock.Object, _context);
        }

        [TestMethod]
        public void Test_GetAllPolls()
        {
            // Arrange
            var existingPoll = new WebApplication1.Models.Poll(
               "1",
               "Test Poll",
               new WebApplication1.Models.Option[] {
                new WebApplication1.Models.Option { ID = "1", Name = "Option 1", Votes = 1 },
                new WebApplication1.Models.Option { ID = "2", Name = "Option 2", Votes = 1 }
               });
            _context.Add(existingPoll);
            _context.SaveChanges();


            var result = _controller.Get();

            WebApplication1.API.Poll[] expected = { new WebApplication1.API.Poll(
                "1",
                "Test Poll",
                new WebApplication1.API.Option[] {
                    new WebApplication1.API.Option { ID = "1", Name = "Option 1", Votes = 1 },
                    new WebApplication1.API.Option { ID = "2", Name = "Option 2", Votes = 1 }
                })
            };

            var resultJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
        }

        [TestMethod]
        public async Task Test_CreatePoll()
        {
            // Arrange

            var toCreate = new WebApplication1.Models.Poll(
               "10",
               "Posted Poll",
               new WebApplication1.Models.Option[] {
                new WebApplication1.Models.Option { ID = "1", Name = "Option 1", Votes = 1 },
                new WebApplication1.Models.Option { ID = "2", Name = "Option 2", Votes = 1 }
               });

            var result = await _controller.CreatePoll(toCreate) as ActionResult<WebApplication1.API.Poll>;
            var createdAtResult = result.Result as CreatedAtActionResult;

            WebApplication1.API.Poll expected = new WebApplication1.API.Poll(
               "10",
               "Posted Poll",
                new WebApplication1.API.Option[] {
                    new WebApplication1.API.Option { ID = "1", Name = "Option 1", Votes = 1 },
                    new WebApplication1.API.Option { ID = "2", Name = "Option 2", Votes = 1 }
                });


            var resultJson = JsonConvert.SerializeObject(createdAtResult.Value);
            var expectedJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
        }
    }
}