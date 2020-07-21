using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBookStore.Areas.Identity.Data;
using MyBookStore.Data;

[assembly: HostingStartup(typeof(MyBookStore.Areas.Identity.IdentityHostingStartup))]
namespace MyBookStore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContextContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDbContextContextConnection")));

                services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddDefaultTokenProviders()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<AuthDbContextContext>();
            });
        }
    }
}