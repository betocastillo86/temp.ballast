using System.Reflection;
using Ballast.Application.Behaviors;
using Ballast.Application.Ports;
using Ballast.Application.Security;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ballast.Application;

public static class ApplicationServiceRegister
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegister).Assembly);

        services.AddSecurityServices();
        return services;
    }
    
    private static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenService, JwtTokenService>();
        
        return services;
    }
}