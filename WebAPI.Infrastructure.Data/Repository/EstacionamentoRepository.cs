using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Model;
using WebAPI.Infrastructure.Data.Data;

namespace WebAPI.Infrastructure.Data.Repository;

public class EstacionamentoRepository : IEstacionamentoRepository
{
    private readonly EstacionamentoDbContext _context;

    public EstacionamentoRepository(EstacionamentoDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Estacionamento>> FindAllAsync()
    {
        return await _context.Estacionamentos.ToListAsync();
    }

    public async Task<Estacionamento> FindByIdAsync(int id)
    {
        return await _context.Estacionamentos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> CreateAsync(Estacionamento estacionamento)
    {
        await _context.Estacionamentos.AddAsync(estacionamento);
        await _context.SaveChangesAsync();

        return estacionamento.Id;
    }

    public async Task<Estacionamento> UpdateAsync(Estacionamento estacionamento)
    {
        if(estacionamento is null)
            throw new ArgumentNullException(nameof(estacionamento), "O objeto estacionamento estava vazio");

        _context.Estacionamentos.Update(estacionamento);
        await _context.SaveChangesAsync();

        return estacionamento;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var estacionamento = await FindByIdAsync(id);

        if (estacionamento is null)
            throw new Exception($"Estacionamento de Id {id} não encontrado");

        _context.Estacionamentos.Remove(estacionamento);
        await _context.SaveChangesAsync();

        return true;
    }
}
