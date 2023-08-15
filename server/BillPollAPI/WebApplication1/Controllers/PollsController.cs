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
            return Enumerable.Range(1, 5).Select(index => new Poll(index.ToString(), "Poll", null)).ToArray();
        }
    }
}