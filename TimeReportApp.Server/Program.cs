using Microsoft.EntityFrameworkCore;
using server.Services;
using TimeReportApp.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = String.Empty;

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("ApplicationConnection");
}
else
{
    connection = Environment.GetEnvironmentVariable("ApplicationConnection");
}

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connection));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add dependancies
builder.Services.AddScoped<ITimeReportService, TimeReportService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IActivityService, ActivityService>();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
