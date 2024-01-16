using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Model;

namespace WebAPI.Infrastructure.Data.Data.Map;

public class VeiculoMap : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Placa).HasMaxLength(100).IsRequired();
    }
}
