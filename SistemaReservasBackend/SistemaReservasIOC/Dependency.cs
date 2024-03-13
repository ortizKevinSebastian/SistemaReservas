using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaReservasBLL.Services;
using SistemaReservasBLL.Services.Contract;
using SistemaReservasDAL;
using SistemaReservasDAL.Repositories;
using SistemaReservasDAL.Repositories.Contract;
using SistemaReservasUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasIOC
{
    public static class Dependency
    {
        public static void DependencyInjection(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<DbReservaContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("dbString"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IReservaRepository, ReservaRepository>();

            //Automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEspacioService, EspacioService>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<ITablaService, TablaService>();
            services.AddScoped<IMenuService, MenuService>();
        } 
    }
}


