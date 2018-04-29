using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Experimentator.Models;
using Experimentator.Infrastructure;

namespace Experimentator
{
    public class Startup
    {
        IConfiguration configuration;
        public Startup(IHostingEnvironment hosting)
        {
            configuration = new ConfigurationBuilder().SetBasePath(hosting.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connect = configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IPasswordValidator<ExperimentatorUser>, CustomPasswordValidator>();
            services.AddDbContext<ExperimentatorDbContext>(options => options.UseSqlServer(connect));
            services.AddIdentity<ExperimentatorUser, IdentityRole>(options=>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<ExperimentatorDbContext>();
            services.AddMvc();
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}", new { controller="Admin", action="Create"});
            }
            );
        }
    }
}
