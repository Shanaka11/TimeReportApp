using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Utils;
using TimeReportApp.Server;

namespace server.Services;

public class ActivityService(ApplicationDbContext context) : IActivityService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Activity>> Get()
    {
        return await _context.Activities.ToListAsync();
    }

    public async Task<Activity?> GetById(int id)
    {
        return await _context.Activities.FindAsync(id);
    }

    public async Task<Activity?> Post(Activity entity)
    {
        _context.Activities.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Put(int id, Activity entity)
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

    public async Task Delete(int id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity == null)
        {
            throw new Exception(ExceptionConsts.NotFound);
        }

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();

        return;
    }

    public bool Exists(int id)
    {
        return _context.Activities.Any(a => a.Id == id);
    }
}
