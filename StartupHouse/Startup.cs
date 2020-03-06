using AspNetCoreRateLimit;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StartupHouse.API.ApiModels;
using StartupHouse.API.Interfaces;
using StartupHouse.API.Interfaces.DTO;
using StartupHouse.API.Middleware;
using StartupHouse.API.Services;
using StartupHouse.Database.Entities;
using StartupHouse.Database.Entities.dbo;
using StartupHouse.Database.Interfaces;
using StartupHouse.Database.Repository;
using System;

namespace StartupHouse
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
            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            
            services.AddControllers();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            var connectionString = Configuration.GetSection("ConnectionString").Get<string>();
            services.AddDbContext<ShDbContext>(options => options
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies());

            services.AddScoped<IContextScope, ContextScope>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddHttpClient<INbpService, NbpService>();

            services.AddScoped(iServiceProvider =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Currency, CurrencyDTO>();
                    cfg.CreateMap<CurrencyDTO, CurrencyApiModel>();
                    cfg.CreateMap<Currency, CurrencyDetailsDTO>()
                        .ForMember(c => c.Prices, options => options.Ignore());
                    cfg.CreateMap<CurrencyDetailsDTO, CurrencyDetailsApiModel>();
                    cfg.CreateMap<CurrencyPrice, CurrencyPriceDTO>();
                    cfg.CreateMap<CurrencyPriceDTO, CurrencyPriceApiModel>();
                });

                var mapper = config.CreateMapper();
                return mapper;
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = Configuration.GetValue<string>("Authentication:Authority");
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddHangfire(x => x.
                UseSqlServerStorage(connectionString));
            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ThrottlingMiddleware>();
            app.UseMiddleware<HttpStatusExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<ICurrencyService>(w => w.UpdateCurrencies(), Cron.Daily(12, 0), TimeZoneInfo.Local);

        }
    }
}
