using WebAPI.Domain.Model;

namespace WebAPI.Domain.Interfaces;

public interface IVeiculoRepository : IRepository<Veiculo>
{
    Task<Veiculo> FindByPlacaAsync(string placa);
}
