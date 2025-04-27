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
    public class TimeReportsController : ControllerBase
    {
        private readonly ITimeReportService _timeReportService;

        public TimeReportsController(ITimeReportService timeReportService)
        {
            _timeReportService = timeReportService;
        }

        // GET: api/TimeReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeReport>>> GetTimeReports([FromQuery] int? ProjectId, [FromQuery] Guid? ClientId, [FromQuery] DateTime? Date)
        {
            if(ProjectId == null && ClientId == null && Date == null)
            {
                return Ok(await _timeReportService.Get());
            }
            return Ok(await _timeReportService.GetTimeReports(ProjectId, ClientId, Date));
        }

        // GET: api/TimeReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeReport>> GetTimeReport(Guid id)
        {
            var timeReport = await _timeReportService.GetById(id);

            if (timeReport == null)
            {
                return NotFound();
            }

            return Ok(timeReport);
        }

        // PUT: api/TimeReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeReport(Guid id, TimeReport timeReport)
        {
            try
            {
                await _timeReportService.Put(id, timeReport);
                return NoContent();
            }
            catch (Exception ex)
            {
                if(ex.Message == ExceptionConsts.BadRequest)
                {
                    return BadRequest();
                }
                if(ex.Message == ExceptionConsts.NotFound)
                {
                    return NotFound();
                }
                throw;
            }
        }

        // POST: api/TimeReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TimeReport>> PostTimeReport(TimeReport timeReport)
        {
            TimeReport? newTimeReport = await _timeReportService.Post(timeReport);

            if (newTimeReport == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetTimeReport", new { id = newTimeReport.Id }, newTimeReport);
        }

        // DELETE: api/TimeReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeReport(Guid id)
        {
            try
            {
                await _timeReportService.Delete(id);
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
