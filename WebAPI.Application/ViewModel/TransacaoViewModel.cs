namespace WebAPI.Application.ViewModel;

public class TransacaoViewModel
{
    public int Id { get; set; }
    public DateTime? HoraEntrada { get; set; }
    public DateTime? HoraSaida { get; set; }
    public double? ValorPago { get; set; }

    public int EstacionamentoId { get; set; }
    public EstacionamentoViewModel? Estacionamento { get; set; }

    public int VeiculoId { get; set; }
    public VeiculoViewModel? Veiculo { get; set; }
}
