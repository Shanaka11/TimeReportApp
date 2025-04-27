
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Utils;
using TimeReportApp.Server;

namespace server.Services;

public class ClientService(ApplicationDbContext context) : IClientService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Client>> Get()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetById(Guid id)
    {
        return await _context.Clients.FindAsync(id);
    }
    public async Task<Client?> Post(Client entity)
    {
        _context.Clients.Add(entity);
        await _context.SaveChangesAsync();
        return await GetById(entity.Id);
    }

    public async Task Put(Guid id, Client entity)
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
    public async Task Delete(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            throw new Exception(ExceptionConsts.NotFound);
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return;
    }

    public bool Exists(Guid id)
    {
        return _context.Clients.Any(e => e.Id == id);
    }
}
