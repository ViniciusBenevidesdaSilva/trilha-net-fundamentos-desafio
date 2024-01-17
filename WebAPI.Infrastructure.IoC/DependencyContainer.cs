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
                mc.AddProfile<EstacionamentoProfile>();
                mc.AddProfile<VeiculoProfile>();
                mc.AddProfile<TransacaoProfile>();
            });

            return mapperConfiguration.CreateMapper();
        });

        // Application Layer
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IEstacionamentoService, EstacionamentoService>();
        services.AddScoped<IVeiculoService, VeiculoService>();
        services.AddScoped<ITransacaoService, TransacaoService>();

        // Data Layer
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IEstacionamentoRepository, EstacionamentoRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>(); 
        services.AddScoped<ITransacaoRepository, TransacaoRepository>();
    }
}