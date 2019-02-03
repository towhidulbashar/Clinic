using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Validation;
using IdentityServer4.Services;

namespace Clinic.Authorize
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
            services.AddIdentityServer()
                .AddSigningCredential("CN=mysite.local")
                .AddTestUsers(Config.GetTestUsers())
                .AddInMemoryClients(new Client[] 
                { 
                    new Client
                    { 
                        ClientId = "reactClient", 
                        ClientName = "React Client",
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,
                        RedirectUris = {"http://localhost:3000/login"},
                        PostLogoutRedirectUris = {"http://localhost:3000/patient"},
                        AllowedCorsOrigins = {"http://localhost:3000"},
                        AllowedScopes = 
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile, 
                            "clinicApi" 
                        },
                        RequireConsent = false
                    }
                })
                .AddInMemoryIdentityResources(new IdentityResource[]
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Email(),
                    new IdentityResources.Profile()
                    /* new IdentityResource
                    {
                        Name = "office",
                        DisplayName = "Office",
                        UserClaims = {"office_number"}
                    } */
                })
                .AddInMemoryApiResources(new ApiResource[]
                {
                    new ApiResource("clinicApi", "Clinic API")
                });
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* app.UseCors(corsPolicyBuilder => 
                corsPolicyBuilder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()); */
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}
