using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Cinema.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Cinema.WebSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<CinemaContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CinemaContext"), 
                        b => b.MigrationsAssembly("Cinema.WebSite")));

            //services.AddDbContext<CinemaContext>(options => options.UseSqlite("Data Source=Movie.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var dbContext = serviceProvider.GetRequiredService<CinemaContext>();

            // var userManager = serviceProvider.GetRequiredService<UserManager<Employee>>();
            DbInitializer.Initialize(dbContext);

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
