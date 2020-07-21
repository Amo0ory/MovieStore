using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBookStore.Data;
using MyBookStore.Areas.Identity.Data;


namespace MyBookStore
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

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddDbContext<ConnectionClass>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")));
            //    services.AddMvc(options => options.EnableEndpointRouting = false);
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContextContext>();
            // services.AddDefaultIdentity<ApplicationUser().AddRoles,IdentityRole>().AddEntityFrameworkStores<AuthDbContextContext>();
           // services.AddIdentity<ApplicationUser, IdentityRole >().AddEntityFrameworkStores<ConnectionClass>();
           
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.UseAuthorization();
            app.UseAuthentication();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        "MoviesByReleaseDate",
            //        "{controller=Movie}/{action=ByReleaseDate}/{id?}");
            //    });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Customer}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        //private async Task CreateRoles(IServiceProvider serviceProvider)
        //{
        //    //initializing custom roles 
        //    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //    string[] roleNames = { "Admin", "Manager", "Member" };
        //    IdentityResult roleResult;

        //    foreach (var roleName in roleNames)
        //    {
        //        var roleExist = await RoleManager.RoleExistsAsync(roleName);
        //        if (!roleExist)
        //        {
        //            //create the roles and seed them to the database: Question 1
        //            roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }
        //}
    }
}
