using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("poll/")]
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
        public ActionResult<IEnumerable<API.Poll>> Get()
        {
            var models = _context.Polls
                .Include(poll => poll.Options) // Eager loading of options
                .OrderBy(poll => poll.Id);

            var polls = new List<API.Poll>();
            foreach (var model in models)
            {
                polls.Add(new API.Poll(model));
            }

            return Ok(polls);
        }

        [HttpGet("{pollId}")]
        public ActionResult<IEnumerable<API.Poll>> Get(int pollId)
        {
            var poll = _context.Polls.Include(poll => poll.Options).FirstOrDefault(p => p.Id == pollId);
            if (poll == null)
            {
                return NotFound();
            }

            return Ok(new API.Poll(poll));
        }

        [HttpPost("{pollId}/{optionId}/vote")]
        public async Task<ActionResult<API.Poll>> Vote(
            int pollId,
            int optionId)
        {
            var poll = _context.Polls.Include(poll => poll.Options).FirstOrDefault(p => p.Id == pollId);
            if (poll == null)
            {
                return NotFound();
            }

            var option = poll.Options.FirstOrDefault(o => o.Id == optionId);
            if (option == null)
            {
                return NotFound();
            }
            option.Votes++;

            _context.Update(option);
            await _context.SaveChangesAsync();
            
            return Ok(new API.Poll(poll));
        }

        [HttpDelete("{pollId}")]
        public async Task <ActionResult<API.Poll>> Delete(int pollId)
        {
            var poll = _context.Polls.FirstOrDefault(p => p.Id == pollId);
            if (poll == null)
            {
                return NotFound();
            }

            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{pollId}/{optionId}")]
        public async Task<ActionResult<API.Poll>> DeleteOption(int pollId)
        {
            var poll = _context.Polls.FirstOrDefault(p => p.Id == pollId);
            if (poll == null)
            {
                return NotFound();
            }

            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost(Name = "Create New Poll")]
        public async Task<ActionResult<API.Poll>> CreatePoll(API.PollCreate poll)
        {
            Models.Poll polltoCreate = new Models.Poll(poll);
            _context.Add(polltoCreate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreatePoll), new { id = polltoCreate.Id }, new API.Poll(polltoCreate));
        }
    }
}