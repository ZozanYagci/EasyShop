using Core.CrossCuttingConcerns.Caching;
using Core.Interfaces;
using DataAccess.Concrete.EntityFramework;
using EasyShop.Api.Services.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace EasyShop.Api.Extensions
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {

            services.AddScoped<IPathProvider, PathProvider>();

            // integration test ortamında değilsek;
            if (!environment.IsEnvironment("IntegrationTest"))
            {

                services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }
            // redis configuration (StackExchange.Redis)
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisConfig = configuration.GetSection("Redis")["Configuration"];
                if (string.IsNullOrWhiteSpace(redisConfig))
                    throw new InvalidOperationException("Redis configuration is missing (Redis:Configuration).");

                var options = ConfigurationOptions.Parse(redisConfig, true);
                options.ResolveDns = true;
                return ConnectionMultiplexer.Connect(options);
            });
            services.TryAddSingleton<ICacheService, RedisCacheManager>();


            // add jwt authentication

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["TokenOptions:Issuer"],  // appsettings.json
                        ValidAudience = configuration["TokenOptions:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenOptions:SecurityKey"]))
                    };
                });

            return services;
        }
    }
}