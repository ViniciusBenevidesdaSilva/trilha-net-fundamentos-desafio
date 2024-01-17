using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Model;
using WebAPI.Infrastructure.Data.Data;

namespace WebAPI.Infrastructure.Data.Repository;

public class TransacaoRepository : ITransacaoRepository
{
    private readonly EstacionamentoDbContext _context;

    public TransacaoRepository(EstacionamentoDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Transacao>> FindAllAsync()
    {
        return await _context.Transacoes.Include(x => x.Estacionamento).Include(x => x.Veiculo).ToListAsync();
    }

    public async Task<IList<Transacao>> FindAllEstacionadosAsync()
    {
        return await _context.Transacoes.Where(x => x.HoraSaida == null).Include(x => x.Estacionamento).Include(x => x.Veiculo).ToListAsync();
    }

    public async Task<IList<Transacao>> FindAllEstacionadosByEstacionamentoIdAsync(int estacionamentoId)
    {
        return await _context.Transacoes.Where(x => x.EstacionamentoId == estacionamentoId && x.HoraSaida == null).Include(x => x.Estacionamento).Include(x => x.Veiculo).ToListAsync();
    }

    public async Task<Transacao> FindByIdAsync(int id)
    {
        return await _context.Transacoes.Include(x => x.Estacionamento).Include(x => x.Veiculo).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<double?> GetReceitaAsync()
    {
        return await _context.Transacoes.SumAsync(x => x.ValorPago);
    }

    public async Task<double?> GetReceitaAsync(DateTime inicio)
    {
        return await _context.Transacoes.Where(x => x.HoraSaida >= inicio).SumAsync(x => x.ValorPago);
    }

    public async Task<double?> GetReceitaAsync(DateTime inicio, DateTime fim)
    {
        return await _context.Transacoes.Where(x => x.HoraSaida >= inicio && x.HoraSaida <= fim).SumAsync(x => x.ValorPago);
    }

    public async Task<Transacao?> IsVeiculoEstacionadoAsync(int veiculoId)
    {
        return await _context.Transacoes.FirstOrDefaultAsync(x => x.VeiculoId == veiculoId && x.HoraSaida == null);
    }

    public async Task<int> CreateAsync(Transacao transacao)
    {
        await _context.Transacoes.AddAsync(transacao);
        await _context.SaveChangesAsync();

        return transacao.Id;
    }

    public async Task<Transacao> UpdateAsync(Transacao transacao)
    {
        if (transacao is null)
            throw new ArgumentNullException(nameof(transacao), "O objeto transação estava vazio");

        _context.Transacoes.Update(transacao);
        await _context.SaveChangesAsync();

        return transacao;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transacao = await FindByIdAsync(id);

        if (transacao is null)
            throw new Exception($"Transação de Id {id} não encontrada");

        _context.Transacoes.Remove(transacao);
        await _context.SaveChangesAsync();

        return true;
    }
}
