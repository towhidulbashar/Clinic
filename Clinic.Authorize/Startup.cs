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
using Clinic.Authorize.Persistance.Repositories;
using Clinic.Authorize.Core.Repositories;
using Clinic.Authorize.Persistance;
using Clinic.Authorize.Core;
using System.Data;
using Npgsql;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddSigningCredential("CN=mysite.local")
                .AddInMemoryClients(new Client[] 
                { 
                    new Client
                    { 
                        ClientId = "reactClient", 
                        ClientName = "Clinic Management",
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,
                        RedirectUris = {"http://localhost:3000/login-return"},
                        PostLogoutRedirectUris = {"http://localhost:3000/Login"},
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
            
            services.AddMvc();
            return services.AddAutofacConfiguration(this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}
