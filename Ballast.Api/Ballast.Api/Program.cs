using Ballast.Api.Filters;
using Ballast.Api.Middleware;
using Ballast.Application;
using Ballast.MemorySql;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting web host");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

    // Add services to the container.

    builder.Services.AddControllers(c =>
    {
        c.Filters.Add(typeof(BusinessExceptionAttribute));
    });

    builder.Services
        .AddApplicationServices()
        .AddSqlRepositories();

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Web host terminated unexpectedly");
}
finally
{
    Log.Information("Web host shut down completed");
    Log.CloseAndFlush();
}