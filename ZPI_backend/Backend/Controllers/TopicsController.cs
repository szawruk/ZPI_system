using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZPI_Database.DataAccess;
using ZPI_Database.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TopicsController : ControllerBase
    {
        private readonly ZPIContext _context;

        public TopicsController(ZPIContext context)
        {
            _context = context;
        }

        // GET: api/Topics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopics()
        {
            return await _context.Topics.ToListAsync();
        }

        // GET: api/Topics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopic(int id)
        {
            var topic = await _context.Topics.FindAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            return topic;
        }

        // GET: api/Topics/5/teams
        [HttpGet("{id}/teams")]
        public async Task<ActionResult<Topic>> GetTopicTeams(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            topic.Teams = _context.Teams.Include(t => t.Promoter).Where(x => x.TopicId == id).ToList();
            if (topic == null)
            {
                return NotFound();
            }

            return topic;
        }

        // GET: api/Topics/5/teams/users
        [HttpGet("{id}/teams/users")]
        public async Task<ActionResult<Topic>> GetTopicTeamsUsers(int id)
        {
            var topic = await _context.Topics.FindAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            topic.Teams = _context.Teams.Include(t => t.Promoter).Where(x => x.TopicId == id).ToList();
            foreach(var team in topic.Teams)
            {
                team.Students = _context.Users.Where(x => x.TeamId == team.Id).ToList();
            }  

            return topic;
        }

        // POST: api/Topics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Topic>> PostTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopic", new { id = topic.Id }, topic);
        }

    }
}
