using Microsoft.EntityFrameworkCore;

using Serilog;

using THH.Services.MainApi;
using THH.Services.MainApi.BLL.Seeding;
using THH.Services.MainApi.DAL.Concrete.EntityFrameworkCore.Contexts;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var startup = new Startup(builder.Environment, builder.Configuration);
    startup.ConfigureServices(builder.Services);
    startup.ConfigureHost(builder.Host);
    Log.Information("Starting host...");
    var app = builder.Build();
    startup.Configure(app);
    using IServiceScope serviceScope = app.Services.CreateScope();
    IServiceProvider services = serviceScope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    DefaultsSeeder seeder = services.GetRequiredService<DefaultsSeeder>();
    await context.Database.MigrateAsync();
    await seeder.SeedAsync();
    await app.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}