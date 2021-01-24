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
    public class ProjectTasksController : ControllerBase
    {
        private readonly ZPIContext _context;

        public ProjectTasksController(ZPIContext context)
        {
            _context = context;
        }

        // GET: api/ProjectTasks/Team/2
        [HttpGet("Team/{teamId}")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks(int teamId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.TeamId == teamId);

            if(user == null)
            {
                ModelState.AddModelError("", "Nie masz dostępu do tego zespołu");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            var tasks = _context.Tasks.Where(t => t.TeamId == teamId);

            if (tasks == null)
            {
                ModelState.AddModelError("", "Zespół o  takim id nie istnieje");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            foreach(var task in tasks)
            {
                task.Student.Tasks = null;
            }

            return tasks.ToList();
        }

        // GET: api/ProjectTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetProjectTask(int id)
        {
            var projectTask = await _context.Tasks.FindAsync(id);

            if (projectTask == null)
            {
                return NotFound();
            }

            return projectTask;
        }

        // PUT: api/ProjectTasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTask(int id, ProjectTask projectTask)
        {
            if (id != projectTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTaskExists(id))
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

        // POST: api/ProjectTasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProjectTask>> PostProjectTask(TaskTeamStudent taskTeamStudent)
        {
            taskTeamStudent.projectTask.Finished = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            var user = await  _context.Users.FirstOrDefaultAsync(
                x => x.Id == taskTeamStudent.StudentId && x.TeamId == taskTeamStudent.TeamId);

            if(user == null)
            {
                ModelState.AddModelError("", "Wygasła sesja, zaloguj się ponownie");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }
            taskTeamStudent.projectTask.Student = user;
            taskTeamStudent.projectTask.TeamId = user.TeamId;

            _context.Tasks.Add(taskTeamStudent.projectTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectTask", new { id = taskTeamStudent.projectTask.Id }, taskTeamStudent.projectTask);
        }

        // DELETE: api/ProjectTasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectTask>> DeleteProjectTask(int id)
        {
            var projectTask = await _context.Tasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(projectTask);
            await _context.SaveChangesAsync();

            return projectTask;
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
