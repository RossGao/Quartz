using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using WebAppTest.Jobs;
using WebAppTest.Models;
using WebAppTest.Repositories;

namespace WebAppTest
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IDog, Collie>();
            services.AddSingleton<IDogRepo, DogRepo>();
            services.AddSingleton<DoStuffJob>();
            services.AddSingleton<QuartzStartup>();

            services.AddDbContext<DogDBcontext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Dog"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IServiceProvider serProd)

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseExceptionHandler(errorApp => // Universe exception handling.
            {
                errorApp.Run(async context =>   // Add the middleware to handle exception.
                {
                    int errCode = 0;
                    string errMsg = string.Empty;

                    var error = context.Features.Get<IExceptionHandlerFeature>();   // Use the feature to get exception info.
                    if (error != null)
                    {
                        var ex = error.Error;

                        errCode = 500;
                        errMsg = "内部服务异常.";

                        var logger = LogManager.GetCurrentClassLogger();
                        logger.Error(ex, errMsg);
                        context.Response.StatusCode = errCode;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync($"{errMsg}. System exception. Please check log for details");
                    }
                });
            });

            var dog = serProd.GetService<IDog>();
            var dbContext = serProd.GetService<DogDBcontext>();

            var quartz = serProd.GetService<QuartzStartup>();
            //var quartz = new QuartzStartup();
            lifetime.ApplicationStarted.Register(quartz.Start);
            lifetime.ApplicationStopping.Register(quartz.Stop);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
