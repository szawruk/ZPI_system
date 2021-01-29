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

        // GET: api/ProjectTasks/myTeam
        [HttpGet("myTeam")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => "Bearer " + u.Token == authHeader);

            var tasks = _context.Tasks.Where(t => t.TeamId == user.TeamId);

            if (tasks == null)
            {
                ModelState.AddModelError("", "Zespół o  takim id nie istnieje");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            foreach (var task in tasks)
            {
                task.Student.Tasks = null;
            }

            return tasks.ToList();
        }


        // POST: api/ProjectTasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProjectTask>> PostProjectTask(TaskTeam taskTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            var authHeader = Request.Headers["Authorization"].ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => "Bearer " + u.Token == authHeader);

            if (user.TeamId != taskTeam.TeamId)
            {
                ModelState.AddModelError("", "Brak dostępu");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            taskTeam.ProjectTask.Student = user;
            taskTeam.ProjectTask.TeamId = user.TeamId;

            _context.Tasks.Add(taskTeam.ProjectTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectTask", new { id = taskTeam.ProjectTask.Id }, taskTeam.ProjectTask);
        }


    }
}
