using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyTodoCleanApp.Application.Common.Interfaces;
using MyTodoCleanApp.Domain.Entities;
using MyTodoCleanApp.Infrastructure.Authentication;
using MyTodoCleanApp.Infrastructure.Persistence;
using System.Text;

namespace MyTodoCleanApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. JwtSettings-ni appsettings.json-dan o'qish
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        // 2. Database (PostgreSQL)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // 3. Identity sozlamalari
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // 4. Interface-lar
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        // 5. Authentication (JWT tekshiruvi)
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // JwtSettings-ni shu yerning o'zida o'qib olamiz
                var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings?.Secret ?? "default_secret_key_32_characters_long_!"))
                };
            });

        return services;
    }
}