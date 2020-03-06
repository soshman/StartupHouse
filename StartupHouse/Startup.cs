using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StartupHouse.API.ApiModels;
using StartupHouse.API.Interfaces;
using StartupHouse.API.Interfaces.DTO;
using StartupHouse.API.Services;
using StartupHouse.Database.Entities;
using StartupHouse.Database.Entities.dbo;
using StartupHouse.Database.Interfaces;
using StartupHouse.Database.Repository;
using System.Linq;

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
            services.AddControllers();

            var connectionString = Configuration.GetSection("ConnectionString").Get<string>();
            services.AddDbContext<ShDbContext>(options => options
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies());

            services.AddScoped<IContextScope, ContextScope>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICurrencyService, CurrencyService>();

            services.AddScoped(iServiceProvider =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Currency, CurrencyDTO>();
                    cfg.CreateMap<CurrencyDTO, CurrencyApiModel>();
                    cfg.CreateMap<Currency, CurrencyDetailsDTO>();
                    cfg.CreateMap<CurrencyDetailsDTO, CurrencyDetailsApiModel>();
                    cfg.CreateMap<CurrencyPrice, CurrencyPriceDTO>();
                    cfg.CreateMap<CurrencyPriceDTO, CurrencyPriceApiModel>();
                });

                var mapper = config.CreateMapper();
                return mapper;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
