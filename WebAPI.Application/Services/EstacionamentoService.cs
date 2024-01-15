using AutoMapper;
using WebAPI.Application.Interfaces;
using WebAPI.Application.ViewModel;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Services;

public class EstacionamentoService : IEstacionamentoService
{
    private readonly IEstacionamentoRepository _estacionamentoRepository;
    private readonly IMapper _mapper;

    public EstacionamentoService(IEstacionamentoRepository estacionamentoRepository, IMapper mapper)
    {
        _estacionamentoRepository = estacionamentoRepository;
        _mapper = mapper;
    }

    public async Task<IList<EstacionamentoViewModel>> GetEstacionamentosAsync()
    {
        var estacionamentos = await _estacionamentoRepository.FindAllAsync();
        return _mapper.Map<IList<Estacionamento>, IList<EstacionamentoViewModel>>(estacionamentos);
    }

    public async Task<EstacionamentoViewModel> GetEstacionamentoByIdAsync(int id)
    {
        var estacionamento = await _estacionamentoRepository.FindByIdAsync(id);
        return _mapper.Map<Estacionamento, EstacionamentoViewModel>(estacionamento);
    }

    private List<string> ValidaEstacionamento(EstacionamentoViewModel estacionamento)
    {
        var retorno = new List<string>();

        if(estacionamento is null)
        {
            retorno.Add("Estacionamento não pode ser nulo");
            return retorno;
        }

        if (estacionamento.Id < 0)
            retorno.Add("O Id do estacionamento deve ser maior que 0");

        if(estacionamento.PrecoInicial < 0)
            retorno.Add("O Preço inicial deve ser maior ou igual a zero");

        if (estacionamento.PrecoPorHora < 0)
            retorno.Add("O Preço por hora deve ser maior ou igual a zero");

        if (estacionamento.QtdVagas <= 0)
            retorno.Add("A Quantidade de vagas deve ser maior que zero");

        return retorno;
    }

    public async Task<int> CreateAsync(EstacionamentoViewModel estacionamento)
    {
        estacionamento.Id = 0;
        var log = ValidaEstacionamento(estacionamento);

        if (log.Count > 0)
            throw new Exception("Estacionamento inválido: " + String.Join("; ", log));

        var estacionamentoModel = _mapper.Map<EstacionamentoViewModel, Estacionamento>(estacionamento);
        return await _estacionamentoRepository.CreateAsync(estacionamentoModel);
    }

    public async Task<EstacionamentoViewModel> UpdateAsync(EstacionamentoViewModel estacionamento, int id)
    {
        var log = ValidaEstacionamento(estacionamento);

        if (log.Count > 0)
            throw new Exception("Estacionamento inválido: " + String.Join("; ", log));

        var estacionamentoModel = _mapper.Map<EstacionamentoViewModel, Estacionamento>(estacionamento);
        var estacionamentoBanco = await _estacionamentoRepository.FindByIdAsync(id);

        estacionamentoBanco.Descricao = estacionamentoModel.Descricao;
        estacionamentoBanco.PrecoInicial = estacionamentoModel.PrecoInicial;
        estacionamentoBanco.PrecoPorHora = estacionamentoModel.PrecoPorHora;
        estacionamentoBanco.QtdVagas = estacionamentoModel.QtdVagas;

        var retorno = await _estacionamentoRepository.UpdateAsync(estacionamentoBanco);
        return _mapper.Map<Estacionamento, EstacionamentoViewModel>(retorno);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var estacionamentoBanco = await GetEstacionamentoByIdAsync(id);

        if (estacionamentoBanco is null)
            throw new Exception($"Estacionamento de id {id} não encontrado");

        return await _estacionamentoRepository.DeleteAsync(id);
    }
}
