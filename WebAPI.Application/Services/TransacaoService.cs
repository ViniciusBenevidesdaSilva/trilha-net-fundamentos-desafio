using AutoMapper;
using WebAPI.Application.Interfaces;
using WebAPI.Application.ViewModel;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IEstacionamentoRepository _estacionamentoRepository;
    private readonly IMapper _mapper;

    public TransacaoService(ITransacaoRepository transacaoRepository, IVeiculoRepository veiculoRepository, IEstacionamentoRepository estacionamentoRepository, IMapper mapper)
    {
        _transacaoRepository = transacaoRepository;
        _veiculoRepository = veiculoRepository;
        _estacionamentoRepository = estacionamentoRepository;
        _mapper = mapper;
    }

    public async Task<IList<TransacaoViewModel>> GetTransacoesAsync()
    {
        var transacoes = await _transacaoRepository.FindAllAsync();
        return _mapper.Map<IList<Transacao>, IList<TransacaoViewModel>>(transacoes);
    }

    public async Task<TransacaoViewModel> GetTransacaoByIdAsync(int id)
    {
        var transacao = await _transacaoRepository.FindByIdAsync(id);
        return _mapper.Map<Transacao, TransacaoViewModel>(transacao);
    }

    public async Task<IList<TransacaoViewModel>> GetEstacionadosAsync()
    {
        var transacoes = await _transacaoRepository.FindAllEstacionadosAsync();
        return _mapper.Map<IList<Transacao>, IList<TransacaoViewModel>>(transacoes);
    }

    public async Task<IList<TransacaoViewModel>> GetEstacionadosByEstacionamentoIdAsync(int estacionamentoId)
    {
        var transacoes = await _transacaoRepository.FindAllEstacionadosByEstacionamentoIdAsync(estacionamentoId);
        return _mapper.Map<IList<Transacao>, IList<TransacaoViewModel>>(transacoes);
    }

    public async Task<double> GetReceitaAsync()
    {
        var receita = await _transacaoRepository.GetReceitaAsync();
        return receita!.Value;
    }

    public async Task<double> GetReceitaAsync(DateTime inicio)
    {
        var receita = await _transacaoRepository.GetReceitaAsync(inicio); 
        return receita!.Value;
    }

    public async Task<double> GetReceitaAsync(DateTime inicio, DateTime fim)
    {
        var receita = await _transacaoRepository.GetReceitaAsync(inicio, fim); 
        return receita!.Value;
    }

    public async Task<bool> IsVeiculoEstacionadoAsync(int veiculoId)
    {
        return await _transacaoRepository.IsVeiculoEstacionadoAsync(veiculoId) is not null;
    }

    public async Task<bool> IsVeiculoEstacionadoAsync(string veiculoPlaca)
    {
        veiculoPlaca = veiculoPlaca.ToUpper();
        var veiculoId = await _veiculoRepository.FindByPlacaAsync(veiculoPlaca);

        if (veiculoId is null)
            throw new Exception($"Veículo de placa {veiculoPlaca} não encontrado");

        return await IsVeiculoEstacionadoAsync(veiculoId.Id);
    }

    private async Task<List<string>> ValidaTransacao(TransacaoViewModel transacao)
    {
        var retorno = new List<string>();

        if (transacao is null)
        {
            retorno.Add("Transação não pode ser nulo");
            return retorno;
        }

        if(transacao.Id < 0)
            retorno.Add("O Id da transação deve ser maior que 0");

        if (transacao.HoraEntrada > DateTime.Now)
            retorno.Add("A hora de entrada não pode ser posterior à hora atual");

        if(transacao.HoraEntrada is null && transacao.HoraSaida is not null)
            retorno.Add("A hora de entrada não pode ser nula se houver data de saída");

        if (transacao.HoraEntrada >= transacao.HoraSaida)
            retorno.Add("A hora de entrada não pode ser posterior à hora de saída");


        var estacionamentoBanco = await _estacionamentoRepository.FindByIdAsync(transacao.EstacionamentoId);

        if (estacionamentoBanco is null)
            retorno.Add($"Estacionamento de id {transacao.EstacionamentoId} não encontrado");

        var veiculoBanco = await _veiculoRepository.FindByIdAsync(transacao.VeiculoId);

        if (veiculoBanco is null)
            retorno.Add($"Veículo de id {transacao.VeiculoId} não encontrado");

        var transacaoVeiculoEstacionado = await _transacaoRepository.IsVeiculoEstacionadoAsync(transacao.VeiculoId);

        if (transacaoVeiculoEstacionado is not null && transacao.HoraSaida is null && transacaoVeiculoEstacionado.Id != transacao.Id)
            retorno.Add("Esse veículo já está estacionado");

        return retorno;
    }

    private async Task<double?> RetornaValorPago(TransacaoViewModel transacao)
    {
        if (transacao is null)
            return null;

        var estacionamentoBanco = await _estacionamentoRepository.FindByIdAsync(transacao.EstacionamentoId);

        if (transacao.HoraEntrada is null || transacao.HoraSaida is null || estacionamentoBanco is null)
            return null;

        var qtdHoras = Math.Ceiling(transacao.HoraSaida.Value.Subtract(transacao.HoraEntrada.Value).TotalHours);

        var valorPago = estacionamentoBanco.PrecoInicial + estacionamentoBanco.PrecoPorHora * qtdHoras;

        return valorPago;
    }

    public async Task<int> CreateAsync(TransacaoViewModel transacao)
    {
        transacao.Id = 0;
        var log = await ValidaTransacao(transacao);

        if (log.Count > 0)
            throw new Exception("Transação inválida: " + String.Join("; ", log));
        

        transacao.ValorPago = await RetornaValorPago(transacao);

        var transacaoModel = _mapper.Map<TransacaoViewModel, Transacao>(transacao);
        return await _transacaoRepository.CreateAsync(transacaoModel);
    }

    public async Task<TransacaoViewModel> UpdateAsync(TransacaoViewModel transacao, int id)
    {
        var log = await ValidaTransacao(transacao);

        if (log.Count > 0)
            throw new Exception("Transação inválida: " + String.Join("; ", log));

        transacao.ValorPago = await RetornaValorPago(transacao);

        var transacaoModel = _mapper.Map<TransacaoViewModel, Transacao>(transacao);
        var transacaoBanco = await _transacaoRepository.FindByIdAsync(id);

        if (transacaoBanco is null)
            throw new Exception($"Transação de id {id} não encontrada");

        transacaoBanco.HoraEntrada = transacaoModel.HoraEntrada;
        transacaoBanco.HoraSaida = transacaoModel.HoraSaida;
        transacaoBanco.ValorPago = transacaoModel.ValorPago;
        transacaoBanco.EstacionamentoId = transacaoModel.EstacionamentoId;
        transacaoBanco.VeiculoId = transacaoModel.VeiculoId;

        var retorno = await _transacaoRepository.UpdateAsync(transacaoBanco);

        return _mapper.Map<Transacao, TransacaoViewModel>(retorno);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transacaoBanco = await GetTransacaoByIdAsync(id);

        if (transacaoBanco is null)
            throw new Exception($"Transação de id {id} não encontrada");

        return await _transacaoRepository.DeleteAsync(id);
    }
}
