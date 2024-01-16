using WebAPI.Application.ViewModel;

namespace WebAPI.Application.Interfaces;

public interface IVeiculoService
{
    Task<IList<VeiculoViewModel>> GetVeiculosAsync();
    Task<VeiculoViewModel> GetVeiculoByIdAsync(int id); 
    Task<VeiculoViewModel> GetVeiculoByPlacaAsync(string placa);
    Task<int> CreateAsync(VeiculoViewModel veiculo);
    Task<VeiculoViewModel> UpdateAsync(VeiculoViewModel veiculo, int id);
    Task<bool> DeleteAsync(int id);
}
