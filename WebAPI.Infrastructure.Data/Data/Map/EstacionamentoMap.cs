using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Model;

namespace WebAPI.Infrastructure.Data.Data.Map;

public class EstacionamentoMap : IEntityTypeConfiguration<Estacionamento>
{
    public void Configure(EntityTypeBuilder<Estacionamento> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Descricao).HasMaxLength(100).IsRequired(false);
        builder.Property(x => x.PrecoInicial).IsRequired();
        builder.Property(x => x.PrecoPorHora).IsRequired();
        builder.Property(x => x.QtdVagas).IsRequired();
    }
}
