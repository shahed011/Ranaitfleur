using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ranaitfleur.Model;
using Ranaitfleur.Services;
using Ranaitfleur.ViewModels;
using WebMarkupMin.AspNetCore1;

namespace Ranaitfleur
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(_configuration);

            services.AddSingleton(_configuration);
            services.AddScoped<IMailService, MailService>();
            services.AddDbContext<RanaitfleurContext>();
            services.AddScoped<IRanaitfleurRepository, RanaitfleurRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<RanaitfleurContextSeedData>();
            services.AddScoped(SessionCart.GetCart);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddLogging();
            services.AddMvc();
            //services.AddMvc(config =>
            //{
            //    config.Filters.Add(new RequireHttpsAttribute());
            //});
            //.AddJsonOptions(config =>
            //        config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
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
                config.Password.RequireNonAlphanumeric = false;
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

            services.AddWebMarkupMin(
                    options =>
                    {
                        //options.AllowMinificationInDevelopmentEnvironment = true;
                        options.AllowCompressionInDevelopmentEnvironment = true;
                    })
                //.AddHtmlMinification(
                //    options =>
                //    {
                //        options.MinificationSettings.RemoveRedundantAttributes = true;
                //        options.MinificationSettings.RemoveHttpProtocolFromAttributes = true;
                //        options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true;
                //    })
                .AddHttpCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, RanaitfleurContextSeedData seeder)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<ItemViewModel, Item>().ReverseMap();
            });

            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseSession();
            app.UseWebMarkupMin();
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
