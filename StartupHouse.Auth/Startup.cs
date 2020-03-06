using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StartupHouse.Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IResourceOwnerPasswordValidator, PasswordValidator>();

            services.AddIdentityServer(options =>
            {
                options.PublicOrigin = Configuration.GetValue<string>("Server:PublicOrigin");
                options.IssuerUri = Configuration.GetValue<string>("Server:IssuerUri");
                options.Discovery.ShowClaims = false;
            })
            .AddDeveloperSigningCredential()
            .AddInMemoryIdentityResources(new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            })
            .AddInMemoryApiResources(Configuration.GetSection("Resources"))
            .AddInMemoryClients(Configuration.GetSection("Clients"));

            services.AddAuthentication();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseHttpsRedirection();
        }
    }
}
