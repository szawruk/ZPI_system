using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Acefb9Utils;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
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
            return await _context.Teams.Include(t => t.Topic).Include(t => t.Promoter).ToListAsync();
        }

        // GET: api/Teams/myTeam/users
        [HttpGet("myTeam/users")]
        public async Task<ActionResult<Team>> GetTeam()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => "Bearer " + u.Token == authHeader);

            var team = await _context.Teams
                .Include(t => t.Promoter)
                .Include(t => t.Students)
                .FirstOrDefaultAsync(t => t.Id == user.TeamId);

            foreach (var stud in team.Students)
            {
                stud.Team = null;
            }

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // GET: api/Teams/myTeam
        [HttpGet("myTeam")]
        public async Task<ActionResult<Team>> GetTeamOfStudent()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => "Bearer " + u.Token == authHeader);

            if (user.Team == null)
            {
                ModelState.AddModelError("", "Twój rodzaj konta nie może wykonać tej operacji");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            return user.Team;
        }

        // POST: api/Teams
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(TeamTopic tT)
        {
            if (tT == null || tT.Team == null || 
                !ModelState.IsValid || string.IsNullOrWhiteSpace(tT.Team.Name))
            {
                ModelState.AddModelError("", "Nieprawidłowe dane");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }
            var authHeader = Request.Headers["Authorization"].ToString();
            var student = await _context.Users.FirstOrDefaultAsync(u => "Bearer " + u.Token == authHeader);


            if (student.AccountType != AccountType.stud.ToString())
            {
                ModelState.AddModelError("", "Twój rodzaj konta nie może wykonać tej operacji");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }
            if (student.TeamId != null)
            {
                ModelState.AddModelError("", "Już należysz do zespołu!");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }


            if (tT.TopicId != null)
            {
                var topic = await _context.Topics.FindAsync(tT.TopicId);
                if (topic == null)
                {
                    ModelState.AddModelError("", "Taki temat nie istnieje!");
                    return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
                }
                tT.Team.Topic = topic;
            }
            tT.Team.Students.Add(student);

            _context.Teams.Add(tT.Team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = tT.Team.Id }, tT.Team);
        }
    }
}
