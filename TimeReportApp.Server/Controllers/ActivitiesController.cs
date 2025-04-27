using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class ActivitiesController(IActivityService activityService) : ControllerBase
    {
        private readonly IActivityService _activityService = activityService;
        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            return Ok(await _activityService.Get());
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            var activity = await _activityService.GetById(id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, Activity activity)
        {
            try
            {
                await _activityService.Put(id, activity);
                return NoContent();
            }
            catch (Exception ex)
            {
                if(ex.Message == ExceptionConsts.NotFound)
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

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(Activity activity)
        {

            var newActivity = await _activityService.Post(activity);
            if(newActivity == null)
            {
                return NotFound();
            }
            return CreatedAtAction("GetActivity", new { id = newActivity.Id }, newActivity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            try
            {
                await _activityService.Delete(id);
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
