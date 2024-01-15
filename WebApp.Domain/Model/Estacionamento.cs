namespace WebAPI.Domain.Model;

public class Estacionamento : Entity
{
    public string? Descricao { get; set; }
    public double PrecoInicial { get; set; }
    public double PrecoPorHora { get; set; }
    public int QtdVagas { get; set; }
}
