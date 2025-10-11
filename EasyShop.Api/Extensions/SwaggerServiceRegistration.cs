using Microsoft.OpenApi.Models;

namespace EasyShop.Api.Extensions
{
    public static class SwaggerServiceRegistration
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // swagger'a jwt authentication desteği eklendi.
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                 {
                  new OpenApiSecurityScheme
                   {
                      Reference= new OpenApiReference
                      {
                         Type=ReferenceType.SecurityScheme,
                         Id="Bearer"
                    }
                 },
                 new string[]{}
             }
            });
            });

            return services;
        }
    }
}