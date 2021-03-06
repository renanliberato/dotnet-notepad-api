using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using MediatR.Pipeline;
using MediatR;
using WebAPI.Helpers;
using WebAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using WebAPI.Interfaces.Mails;

namespace WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var envSettings = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            services.Configure<AppSettings>(envSettings);
            services.AddScoped<IMailClient, MailClient>();
            services.AddScoped<IAuthService, IdentityAuthService>();

            var appSettings = envSettings.Get<AppSettings>();

            var connection = $"Server={appSettings.DB_HOST};Database={appSettings.DB_NAME};User Id={appSettings.DB_USER};Password={appSettings.DB_PASSWORD};";
            services.AddDbContext<NotepadContext>
                (options => options.UseSqlServer(connection));

            services.AddMediatR();

            // auth
            services.AddAuthentication().AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                ValidIssuer = appSettings.JWT_ISSUER,
                ValidAudience = appSettings.JWT_ISSUER,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT_TOKEN))
                };
            });

            services
                .AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<NotepadContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var context = provider.GetService<NotepadContext>())
            {
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
