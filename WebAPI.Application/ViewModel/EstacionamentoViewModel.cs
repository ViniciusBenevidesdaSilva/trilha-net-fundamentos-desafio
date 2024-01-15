namespace WebAPI.Application.ViewModel;

public class EstacionamentoViewModel
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public double PrecoInicial { get; set; }
    public double PrecoPorHora { get; set; }
    public int QtdVagas { get; set; }
}
