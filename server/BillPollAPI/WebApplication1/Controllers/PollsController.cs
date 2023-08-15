using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.API;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollsController : ControllerBase
    {
        private readonly ILogger<PollsController> _logger;
        private readonly PollContext _context; // Inject your DbContext here

        public PollsController(ILogger<PollsController> logger, PollContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetAllPolls")]
        public IEnumerable<API.Poll> Get()
        {
            var models = _context.Polls
                .Include(poll => poll.Options) // Eager loading of options
                .OrderBy(poll => poll.Id);

            var polls = new List<API.Poll>();
            foreach (var model in models)
            {
                polls.Add(new API.Poll(model));
            }
            return polls;
        }
    }
}