using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(conf =>
            {
                var connStr = configuration["EasyShopDbConnectionString"].ToString();
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure(); // bağlantı başarısız olursa tekrar bağlanmayı dene.
                });
            });



            return services;
        }
    }
}
