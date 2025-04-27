using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server;
using server.Models;
using server.Services;
using server.Utils;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return Ok(await _projectService.Get());
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            try
            {
                await _projectService.Put(id, project);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message == ExceptionConsts.NotFound)
                {
                    return NotFound();
                }
                if(ex.Message == ExceptionConsts.BadRequest)
                {
                    return BadRequest();
                }
                throw;
            }
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            
            var newProject = await _projectService.Post(project);
            if (newProject == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetProject", new { id = newProject.Id }, newProject);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if(ex.Message == ExceptionConsts.NotFound)
                {
                    return NotFound();
                }
                throw;
            }
        }
    }
}
