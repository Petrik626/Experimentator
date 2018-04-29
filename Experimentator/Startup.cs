using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Experimentator.Models;

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
            services.AddDbContext<ExperimentatorDbContext>(options => options.UseSqlServer(connect));
            services.AddIdentity<ExperimentatorUser, IdentityRole>().AddEntityFrameworkStores<ExperimentatorDbContext>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();
        }
    }
}
