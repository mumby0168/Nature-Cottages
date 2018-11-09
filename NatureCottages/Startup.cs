using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<CottageDbContext>(options => { options.UseSqlServer(dbConnectionString); });

            services.AddScoped<IAttractionRepository, AttractionRepository>();
            services.AddScoped<ICottageRepository, CottageRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {

            var dbcon = serviceProvider.GetService<CottageDbContext>();

            var list = dbcon.Cottages.ToList();

            foreach (var cottage in list)
            {
                cottage.Description =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation";
            }

            dbcon.SaveChanges();
            

            app.UseMvcWithDefaultRoute();

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
       
        }
    }
}
