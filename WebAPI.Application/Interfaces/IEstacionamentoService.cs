using WebAPI.Application.ViewModel;

namespace WebAPI.Application.Interfaces;

public interface IEstacionamentoService
{
    Task<IList<EstacionamentoViewModel>> GetEstacionamentosAsync();
    Task<EstacionamentoViewModel> GetEstacionamentoByIdAsync(int id);
    Task<int> CreateAsync (EstacionamentoViewModel estacionamento);
    Task<EstacionamentoViewModel> UpdateAsync(EstacionamentoViewModel estacionamento, int id);
    Task<bool> DeleteAsync(int id);
}
