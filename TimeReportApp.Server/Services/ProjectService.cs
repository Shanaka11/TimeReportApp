using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Utils;
using TimeReportApp.Server;

namespace server.Services;

public class ProjectService(ApplicationDbContext context) : IProjectService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Project>> Get()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project?> GetById(int id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<Project?> Post(Project entity)
    {
        _context.Projects.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Put(int id, Project entity)
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
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            throw new Exception(ExceptionConsts.NotFound);
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return;
    }

    public bool Exists(int id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }
}
