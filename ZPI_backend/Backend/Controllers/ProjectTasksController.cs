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
        public async Task<ActionResult<ProjectTask>> PostProjectTask(TaskUser task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            var authHeader = Request.Headers["Authorization"].ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => "Bearer " + u.Token == authHeader);

            if (user.TeamId ==null)
            {
                ModelState.AddModelError("", "Nie posiadasz zespołu, aby dodać zadanie");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            var userToAssign = await _context.Users.FirstOrDefaultAsync(u => u.Id == task.StudentId && u.TeamId == user.TeamId);

            if (userToAssign == null)
            {
                ModelState.AddModelError("", "Taki użytkownik nie istnieje w twoim zespole");
                return BadRequest(ErrorFunctionality.ObjectErrorReturn(400, ModelState.Values));
            }

            task.ProjectTask.Student = userToAssign;
            task.ProjectTask.TeamId = user.TeamId;

            _context.Tasks.Add(task.ProjectTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostProjectTask", new { id = task.ProjectTask.Id }, task.ProjectTask);
        }


    }
}
