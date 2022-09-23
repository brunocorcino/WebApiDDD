using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiDDD.Application.AppServices;
using WebApiDDD.Application.Interfaces;
using WebApiDDD.Domain.Interfaces.Repositories;
using WebApiDDD.Domain.Interfaces.Services;
using WebApiDDD.Domain.Services;
using WebApiDDD.Infra.Data.Context;
using WebApiDDD.Infra.Data.Repositories;

namespace WebApiDDD.Infra.IoC
{
    public static class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WebApiDDDContext>(options => options.UseSqlServer(connectionString));

            // Carros
            services.AddScoped<ICarrosRepository, CarrosRepository>();
            services.AddScoped<ICarrosService, CarrosService>();
            services.AddScoped<ICarrosAppService, CarrosAppService>();

            // Marcas
            services.AddScoped<IMarcasRepository, MarcasRepository>();
            services.AddScoped<IMarcasService, MarcasService>();
            services.AddScoped<IMarcasAppService, MarcasAppService>();
        }
    }
}
