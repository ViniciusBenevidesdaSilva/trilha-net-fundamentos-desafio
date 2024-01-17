using WebAPI.Application.ViewModel;

namespace WebAPI.Application.Interfaces;

public interface ITransacaoService
{
    Task<IList<TransacaoViewModel>> GetTransacoesAsync();
    Task<TransacaoViewModel> GetTransacaoByIdAsync(int id);
    Task<IList<TransacaoViewModel>> GetEstacionadosAsync();
    Task<IList<TransacaoViewModel>> GetEstacionadosByEstacionamentoIdAsync(int estacionamentoId);

    Task<double> GetReceitaAsync();
    Task<double> GetReceitaAsync(DateTime inicio);
    Task<double> GetReceitaAsync(DateTime inicio, DateTime fim);

    Task<bool> IsVeiculoEstacionadoAsync(int veiculoId);
    Task<bool> IsVeiculoEstacionadoAsync(string veiculoPlaca);

    Task<TransacaoViewModel> RegistrarEntradaAsync(string placaVeiculo, int estacionamentoId, DateTime? entrada = null);
    Task<TransacaoViewModel> RegistrarSaidaAsync(string placaVeiculo, DateTime? saida = null);


    Task<int> CreateAsync(TransacaoViewModel transacao);
    Task<TransacaoViewModel> UpdateAsync(TransacaoViewModel transacao, int id);
    Task<bool> DeleteAsync(int id);
}
