using Erfa.ProductionManagement.Api;
using Erfa.ProductionManagement.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetService<ErfaDbContext>();
if (context != null)
{
    context.Database.Migrate();
}

app.Run();

public partial class Program { }