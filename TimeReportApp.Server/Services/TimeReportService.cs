using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Utils;
using TimeReportApp.Server;

namespace server.Services;

public class TimeReportService(ApplicationDbContext context) : ITimeReportService
{
    private readonly ApplicationDbContext _context = context;

    public async Task Delete(Guid id)
    {
        var timeReport = await _context.TimeReports.FindAsync(id);
        if (timeReport == null)
        {
            throw new Exception(ExceptionConsts.NotFound);
        }

        _context.TimeReports.Remove(timeReport);
        await _context.SaveChangesAsync();

        return;
    }

    public async Task<TimeReport?> GetById(Guid id)
    {
        var timeReport = await _context.TimeReports.FindAsync(id);

        return timeReport;
    }

    public async Task<IEnumerable<TimeReport>> GetTimeReports([FromQuery] int? ProjectId, [FromQuery] Guid? ClientId, [FromQuery] DateTime? Date)
    {
        return await _context.TimeReports.ToListAsync();
    }

    public async Task<IEnumerable<TimeReport>> Get()
    {
        return await _context.TimeReports.ToListAsync();
    }

    public async Task<TimeReport?> Post(TimeReport entity)
    {
        _context.TimeReports.Add(entity);
        await _context.SaveChangesAsync();

        return await GetById(entity.Id);
    }

    public async Task Put(Guid id, TimeReport entity)
    {
        if (id != entity.Id)
        {
            throw new Exception(ExceptionConsts.BadRequest);
        }

        _context.Entry(entity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!Exists(id))
            {
                throw new Exception(ExceptionConsts.NotFound);
            }
            else
            {
                throw;
            }
        }
        return;
    }

    public bool Exists(Guid id)
    {
        return _context.TimeReports.Any(e => e.Id == id);
    }


}
