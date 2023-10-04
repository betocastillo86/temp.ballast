using System.Text;
using Ballast.Api.Filters;
using Ballast.Api.Middleware;
using Ballast.Application;
using Ballast.MemorySql;
using Microsoft.IdentityModel.Tokens;
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
    
    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"]
            };
            options.Audience = builder.Configuration["Jwt:Audience"];
            options.ClaimsIssuer = builder.Configuration["Jwt:Issuer"];
        });

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