using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: api/ProjectTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
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
        public async Task<ActionResult<ProjectTask>> PostProjectTask(ProjectTask projectTask)
        {
            _context.Tasks.Add(projectTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectTask", new { id = projectTask.Id }, projectTask);
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
