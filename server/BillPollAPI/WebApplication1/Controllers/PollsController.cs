using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollsController : ControllerBase
    {
        private readonly ILogger<PollsController> _logger;

        public PollsController(ILogger<PollsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllPolls")]
        public IEnumerable<Poll> Get()
        {
            Option[] options = { new Option("1", "Option 1", 1), new Option("1", "Option 2", 1) };
            Poll[] polls = { new Poll("1", "Test Poll", options) };
            return polls;   
        }
    }
}