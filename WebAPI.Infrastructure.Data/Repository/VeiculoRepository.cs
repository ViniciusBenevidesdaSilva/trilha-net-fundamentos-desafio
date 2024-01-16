using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Model;
using WebAPI.Infrastructure.Data.Data;

namespace WebAPI.Infrastructure.Data.Repository;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly EstacionamentoDbContext _context;

    public VeiculoRepository(EstacionamentoDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Veiculo>> FindAllAsync()
    {
        return await _context.Veiculos.ToListAsync();
    }

    public async Task<Veiculo> FindByIdAsync(int id)
    {
        return await _context.Veiculos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Veiculo> FindByPlacaAsync(string placa)
    {
        return await _context.Veiculos.FirstOrDefaultAsync(x => x.Placa.ToUpper() == placa.ToUpper());
    }

    public async Task<int> CreateAsync(Veiculo veiculo)
    {
        await _context.Veiculos.AddAsync(veiculo);
        await _context.SaveChangesAsync();

        return veiculo.Id;
    }

    public async Task<Veiculo> UpdateAsync(Veiculo veiculo)
    {
        if (veiculo is null)
            throw new ArgumentNullException(nameof(veiculo), "O objeto veículo estava vazio");

        _context.Veiculos.Update(veiculo);
        await _context.SaveChangesAsync();

        return veiculo;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var veiculo = await FindByIdAsync(id);

        if (veiculo is null)
            throw new Exception($"Veiculo de Id {id} não encontrado");

        _context.Veiculos.Remove(veiculo);
        await _context.SaveChangesAsync();

        return true;
    }
}
