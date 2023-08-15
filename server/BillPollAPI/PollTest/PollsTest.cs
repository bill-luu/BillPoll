using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using WebApplication1.Controllers;
using WebApplication1.API;
using Microsoft.EntityFrameworkCore;
using WebApplication1;

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
            _context = new PollContext();
            _context.DbPath =  Path.GetFullPath("../../../testdata/poll.db");
            _controller = new PollsController(_loggerMock.Object, _context);
        }
        [TestMethod]
        public void Test_GetAllPolls()
        {
            var result = _controller.Get();

            Option[] options = {
                new Option { ID = "1", Name = "Option 1", Votes = 1 },
                new Option { ID = "2", Name = "Option 2", Votes = 1 }
            };


            Poll[] expected = { new Poll("1", "Test Poll", options) };

            var resultJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
        }
    }
}