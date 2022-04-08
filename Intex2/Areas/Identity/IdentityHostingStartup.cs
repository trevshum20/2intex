using System;
using Intex2.Areas.Identity.Data;
using Intex2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Intex2.Areas.Identity.IdentityHostingStartup))]
namespace Intex2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            string endpoint = Environment.GetEnvironmentVariable("connection_string");

            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseMySql(endpoint));

                services.AddDefaultIdentity<ApplicationUser>(options =>
                {

                  
                    //services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;

                    



                }).AddRoles<IdentityRole>()
                
                    .AddEntityFrameworkStores<AuthDbContext>();
            });
        }
    }


}