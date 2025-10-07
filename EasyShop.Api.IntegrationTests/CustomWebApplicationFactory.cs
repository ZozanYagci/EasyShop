using Core.CrossCuttingConcerns.Caching;
using DataAccess.Concrete.EntityFramework;
using EasyShop.Api.IntegrationTests.Seed;
using EasyShop.Api.IntegrationTests.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace EasyShop.Api.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("IntegrationTest");

            //builder.ConfigureAppConfiguration(config =>
            //{
            //    config.AddJsonFile("appsettings.Test.json");
            //});


            builder.ConfigureServices(services =>
            {

                // production için register edilmiş olan gerçek DbContext'i kaldır

                var dbContextDescriptors = services.Where(d => d.ServiceType == typeof(DbContextOptions<Context>)).ToList();

                foreach (var descriptor in dbContextDescriptors)
                {
                    services.Remove(descriptor);
                }


                services.AddDbContext<Context>(options =>
                {
                    options.UseInMemoryDatabase("TestDb"); 
                });


                services.RemoveAll<ICacheService>(); 

                services.AddSingleton<ICacheService, InMemoryCacheService>();
            });
        }

        // test başına db resetleme ve seed
        public void ResetDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var seeder = new DefaultTestDataSeeder();
            seeder.Seed(db);
        }
    }
}
