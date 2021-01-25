using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Acefb9Utils;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZPI_Database.DataAccess;
using ZPI_Database.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ZPIContext _context;

        public TeamsController(ZPIContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams.Include(x => x.Topic).Include(x => x.Promoter).ToListAsync();
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams
                .Include(t => t.Tasks)
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.Id == id);

            foreach(var stud in team.Students)
            {
                stud.Team = null;
            }

            foreach (var task in team.Tasks)
            {
                task.Student.Tasks = null;
            }

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // GET: api/Teams/myTeam/5
        [HttpGet("myTeam/{id}")]
        public async Task<ActionResult<Team>> GetTeamOfStudent(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            //var team = await _context.Teams.Include(t => t.Tasks);
            if (user.Team == null)
            {
                return NotFound();
            }

            return user.Team;
        }

        // GET: api/Teams/5/users
        [HttpGet("{id}/users")]
        public async Task<ActionResult<Team>> GetTeamWithStudents(int id)
        {
            var team = await _context.Teams.Include(t => t.Students).FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (!ModelState.IsValid)
            {
                string error = ErrorFunctionality.ErrorsToString(ModelState.Values);
                return BadRequest(new { StatusCode = 400, error });
            }
            if (id != team.Id)
            {
                ModelState.AddModelError("", "");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Teams
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(TeamStudentTopic tsT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }
            var student = await _context.Users.FindAsync(tsT.StudentId);
            var topic = await _context.Topics.FindAsync(tsT.TopicId);

            if (student == null) {
                ModelState.AddModelError("","Wygasła sesja, zaloguj się ponownie");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            if (student.AccountType != AccountType.stud.ToString())
            {
                ModelState.AddModelError("", "Twój rodzaj konta nie może wykonać tej operacji");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            tsT.Team.Students.Add(student);
            tsT.Team.Topic = topic;
            _context.Teams.Add(tsT.Team);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetTeam", new { id = tsT.Team.Id }, tsT.Team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return team;
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
