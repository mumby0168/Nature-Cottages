using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NatureCottages.Database;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Persitance;
using NatureCottages.Database.Repositorys.DomainRepositorys;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.Services.Interfaces;
using NatureCottages.Services.Services;


namespace NatureCottages
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(routes =>
            {
                //add routes here.
            });


            string dbConnectionString =
                @"server=176.32.230.252;port=3306;pwd=NatureCottages123;uid=cl57-mumby0168;database=cl57-mumby0168;;";

            services.AddDbContext<CottageDbContext>(options => { options.UseMySql(dbConnectionString); });

            services.AddScoped<IAttractionRepository, AttractionRepository>();
            services.AddScoped<ICottageRepository, CottageRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IImageGroupRepository, ImageGroupRepository>();
            services.AddScoped<IMailServerService, MailServersService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordProtectionService, PasswordProtectionService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                    options =>
                    {                        
                        options.LoginPath = "/Account/Login";
                        options.LogoutPath = "/Account/Logout";
                    });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {

            var dbContext = serviceProvider.GetService<CottageDbContext>();            

            dbContext.SaveChanges();
            dbContext.Dispose();

            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            
            app.UseMvcWithDefaultRoute();            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
       
        }
    }
}
