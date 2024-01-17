namespace WebAPI.Domain.Model;

public class Transacao : Entity
{
    public DateTime? HoraEntrada { get; set; }
    public DateTime? HoraSaida { get; set; }
    public double? ValorPago { get; set; }

    public int EstacionamentoId { get; set; }
    public Estacionamento Estacionamento { get; set; }

    public int VeiculoId { get; set; }
    public Veiculo Veiculo { get; set; }
}
