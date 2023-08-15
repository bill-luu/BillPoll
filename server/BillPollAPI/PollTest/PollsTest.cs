using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace PollTest
{
    [TestClass]
    public class PollsTest
    {
        private Mock<ILogger<PollsController>>? _loggerMock;
        private PollsController? _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _loggerMock = new Mock<ILogger<PollsController>>();
            _controller = new PollsController(_loggerMock.Object);
        }
        [TestMethod]
        public void Test_GetAllPolls()
        {
            var result = _controller.Get();
            Option[] options = { new Option("1", "Option 1", 1), new Option("1", "Option 2", 1) };
            Poll[] expected = { new Poll("1", "Test Poll", options) };

            var resultJson = JsonConvert.SerializeObject(result);
            var expectedJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
        }
    }
}