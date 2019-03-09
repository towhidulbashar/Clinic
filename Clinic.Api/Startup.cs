using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Clinic.Api.Persistance.Repositories;
using Clinic.Api.Core.Repositories;
using Clinic.Api.Persistance;
using Clinic.Api.Core;
using Clinic.Api.Middlewares;

namespace Clinic.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(option => 
            {
                option.Authority = "http://localhost:51000"; //Who do we trust
                option.RequireHttpsMetadata = false; //Just for development
                option.ApiName = "clinicApi";
            });
            /* .AddAuthentication(options => 
            {
                options.DefaultScheme =  "";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddJwtBearer(jwt => 
            {
                
                jwt.Audience= "clinicApi"; //Who are we
                jwt.Authority = "http://localhost:51000"; //Who do we trust
                jwt.RequireHttpsMetadata = false; //Just for development
            
            })
            .AddOpenIdConnect("oidc", options => 
            {                
                options.SignInScheme = "";
                options.Authority = "http://localhost:51000/";
                options.RequireHttpsMetadata = false;
                options.ClientId = "react client"; //who am I
                options.ResponseType = "";
                options.SaveTokens = true;
            }); */
            return services.AddAutofacConfiguration(this.Configuration);
        }        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseErrorHandlingMiddleware();
            app.UseCors(corsPolicyBuilder =>
                corsPolicyBuilder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();            
            app.UseMvc();
        }
    }
}
