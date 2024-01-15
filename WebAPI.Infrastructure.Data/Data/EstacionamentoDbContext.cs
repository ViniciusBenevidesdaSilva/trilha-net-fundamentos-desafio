using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Model;
using WebAPI.Infrastructure.Data.Data.Map;

namespace WebAPI.Infrastructure.Data.Data;

public class EstacionamentoDbContext : DbContext
{
    public EstacionamentoDbContext(DbContextOptions<EstacionamentoDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMap());
        base.OnModelCreating(modelBuilder);
    }
}
