using Microsoft.EntityFrameworkCore;
using server.Models;

namespace TimeReportApp.Server;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<TimeReport> TimeReports { get; set; }
}
