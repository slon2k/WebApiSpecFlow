using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Linq;
using Api.Data;

namespace SpecFlowTests
{
    public class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DataContext>));

                services.Remove(descriptor);

                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("lib-db");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DataContext>();

                    db.Database.EnsureCreated();

                    try
                    {
                        DbInitializer.SeedData(db);
                    }
                    catch
                    {
                        throw;
                    }
                }
            });
        }
    }
}
