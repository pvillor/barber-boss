using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Domain.Repositories.Users;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Domain.Services;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Infrastructure.Services;
using BarberBoss.Infrastructure.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddToken(services, configuration);
        AddRepositories(services);

        services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    public static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBillingsReadOnlyRepository, BillingsRepository>();
        services.AddScoped<IBillingsWriteOnlyRepository, BillingsRepository>();
        services.AddScoped<IBillingsUpdateOnlyRepository, BillingsRepository>();

        services.AddScoped<IUserReadOnlyRepository, UsersRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UsersRepository>();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        var version = new Version(8, 4, 4);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
