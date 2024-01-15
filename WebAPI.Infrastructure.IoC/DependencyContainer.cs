using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces;
using WebAPI.Application.Mapper.Profiles;
using WebAPI.Application.Services;
using WebAPI.Domain.Interfaces;
using WebAPI.Infrastructure.Data.Data;
using WebAPI.Infrastructure.Data.Repository;

namespace WebAPI.Infrastructure.IoC;

public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // Entity Framework
        services.AddEntityFrameworkSqlServer()
            .AddDbContext<EstacionamentoDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DataBase"))
            );

        // Auto Mapper
        services.AddSingleton<IMapper>(r => 
        {
            var mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile<UsuarioProfile>();
            });

            return mapperConfiguration.CreateMapper();
        });

        // Application Layer
        services.AddScoped<IUsuarioService, UsuarioService>();

        // Data Layer
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}