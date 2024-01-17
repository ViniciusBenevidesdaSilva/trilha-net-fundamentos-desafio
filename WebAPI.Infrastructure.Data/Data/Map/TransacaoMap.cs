using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Model;

namespace WebAPI.Infrastructure.Data.Data.Map;

public class TransacaoMap : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.HoraEntrada).IsRequired(false);
        builder.Property(x => x.HoraSaida).IsRequired(false);
        builder.Property(x => x.ValorPago).IsRequired(false);

        builder.Property(x => x.EstacionamentoId).IsRequired();
        builder.Property(x => x.VeiculoId).IsRequired();

        builder.HasOne(x => x.Estacionamento);
        builder.HasOne(x => x.Veiculo);
    }
}
