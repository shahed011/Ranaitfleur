using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ranaitfleur.Model;
using Ranaitfleur.Services;
using Ranaitfleur.ViewModels;

namespace Ranaitfleur
{
    public class Startup
    {
        private readonly IConfigurationRoot _config;
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddScoped<IMailService, MailService>();
            services.AddDbContext<RanaitfleurContext>();
            services.AddScoped<IRanaitfleurRepository, RanaitfleurRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<RanaitfleurContextSeedData>();
            services.AddScoped(SessionCart.GetCart);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddLogging();
            services.AddMvc(config =>
            {
                config.Filters.Add(new RequireHttpsAttribute());
            });
            //.AddJsonOptions(config =>
            //        config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddIdentity<RanaitfleurUser, IdentityRole>(config =>
            {
                // Sign in settings
                //config.SignIn.RequireConfirmedEmail = true;

                // User settings
                config.User.RequireUniqueEmail = true;

                // Lockout settings
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Password.RequiredLength = 8;
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequireUppercase = true;

                // Cookie settings
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                config.Cookies.ApplicationCookie.LogoutPath = "/App/Index";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            })
            .AddEntityFrameworkStores<RanaitfleurContext>()
            .AddDefaultTokenProviders();

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            RanaitfleurContextSeedData seeder)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<ItemViewModel, Item>().ReverseMap();
            });

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }
            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseSession();
            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" });
            });

            seeder.EnsureSeedData().Wait();
        }
    }
}
