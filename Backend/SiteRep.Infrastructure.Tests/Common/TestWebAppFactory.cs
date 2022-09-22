using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SitRep.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteRep.Infrastructure.Tests.Common
{
    public class TestingWebAppFactory<T> : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SitRepContext>));
                if (dbContext != null)
                    services.Remove(dbContext);

                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<SitRepContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });
                var sp = services.BuildServiceProvider();

                //using (var scope = sp.CreateScope())
                //{
                //    using (var appContext = scope.ServiceProvider.GetRequiredService<SitRepContext>())
                //    {
                //        try
                //        {
                //            appContext.Database.EnsureDeleted();
                //            appContext.Database.EnsureCreated();
                //        }
                //        catch (Exception ex)
                //        {
                //            //Log errors
                //            throw;
                //        }
                //    }
                //}
            });
        }
    }
}
