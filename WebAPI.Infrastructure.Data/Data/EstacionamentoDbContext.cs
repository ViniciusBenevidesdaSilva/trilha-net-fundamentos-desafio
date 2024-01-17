using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Model;
using WebAPI.Infrastructure.Data.Data.Map;

namespace WebAPI.Infrastructure.Data.Data;

public class EstacionamentoDbContext : DbContext
{
    public EstacionamentoDbContext(DbContextOptions<EstacionamentoDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Estacionamento> Estacionamentos { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMap());
        modelBuilder.ApplyConfiguration(new EstacionamentoMap());
        modelBuilder.ApplyConfiguration(new VeiculoMap());
        modelBuilder.ApplyConfiguration(new TransacaoMap());

        base.OnModelCreating(modelBuilder);
    }
}
