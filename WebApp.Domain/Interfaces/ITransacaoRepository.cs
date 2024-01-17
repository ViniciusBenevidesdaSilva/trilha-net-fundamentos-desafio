using WebAPI.Domain.Model;

namespace WebAPI.Domain.Interfaces;

public interface ITransacaoRepository : IRepository<Transacao>
{
    Task<IList<Transacao>> FindAllEstacionadosAsync();
    Task<IList<Transacao>> FindAllEstacionadosByEstacionamentoIdAsync(int estacionamentoId);

    Task<double?> GetReceitaAsync();
    Task<double?> GetReceitaAsync(DateTime inicio);
    Task<double?> GetReceitaAsync(DateTime inicio, DateTime fim);

    Task<Transacao?> IsVeiculoEstacionadoAsync(int veiculoId);
}
