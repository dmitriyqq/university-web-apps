using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MoviePicker.Config;
using MoviePicker.Middlewares;
using MoviePicker.Services;

namespace MoviePicker
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
            services.Configure<MongoOptions>(Configuration.GetSection("mongoConnection"));
            services.Configure<OMDBConfig>(Configuration.GetSection("omdb"));

            services.AddControllersWithViews();
            services.AddSingleton<IMovieCollectionsHistoryService, MovieCollectionsHistoryService>();
            services.AddSingleton<IMovieCollectionsService, MovieCollectionsService>();
            services.AddSingleton<IMoviesService, MoviesService>();
            services.AddSingleton<IOMDBService, OMDBService>();
            services.AddSingleton<IUsersService, UsersService>();

            services.AddHttpClient("omdb", c =>
            {
                var resource = Configuration.GetSection("omdb").GetValue<string>("Resource");
                if (string.IsNullOrWhiteSpace(resource))
                {
                    throw new System.Exception("omdb not configured");
                }
                c.BaseAddress = new System.Uri(resource);
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../movie-picker-client/build";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMiddleware<LoggerMiddleware>();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../movie-picker-client";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
