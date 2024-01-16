using AutoMapper;
using WebAPI.Application.Interfaces;
using WebAPI.Application.ViewModel;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Model;
using WebAPI.Domain.Utils;

namespace WebAPI.Application.Services;

public class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IMapper _mapper;

    public VeiculoService(IVeiculoRepository veiculoRepository, IMapper mapper)
    {
        _veiculoRepository = veiculoRepository;
        _mapper = mapper;
    }

    public async Task<IList<VeiculoViewModel>> GetVeiculosAsync()
    {
        var veiculos = await _veiculoRepository.FindAllAsync();
        return _mapper.Map<IList<Veiculo>, IList<VeiculoViewModel>>(veiculos);
    }

    public async Task<VeiculoViewModel> GetVeiculoByIdAsync(int id)
    {
        var veiculo = await _veiculoRepository.FindByIdAsync(id);
        return _mapper.Map<Veiculo, VeiculoViewModel>(veiculo);
    }

    public async Task<VeiculoViewModel> GetVeiculoByPlacaAsync(string placa)
    {
        var veiculo = await _veiculoRepository.FindByPlacaAsync(placa);
        return _mapper.Map<Veiculo, VeiculoViewModel>(veiculo);
    }

    private async Task<List<string>> ValidaVeiculo(VeiculoViewModel veiculo)
    {
        var retorno = new List<string>();

        if(veiculo is null)
        {
            retorno.Add("Veículo não pode ser nulo");
            return retorno;
        }

        if (veiculo.Id < 0)
            retorno.Add("O Id do estacionamento deve ser maior que 0");

        veiculo.Placa = veiculo.Placa.ToUpper();

        if(!Regex.ValidaFormatoPlaca(veiculo.Placa))
            retorno.Add("Formato de placa inválido");

        var veiculoPlaca = await GetVeiculoByPlacaAsync(veiculo.Placa);

        if(veiculoPlaca is not null)
        {
            if (veiculo.Id == 0 || veiculo.Id != veiculoPlaca.Id)
                retorno.Add("Placa já cadastrada");
        }

        return retorno;
    }

    public async Task<int> CreateAsync(VeiculoViewModel veiculo)
    {
        veiculo.Id = 0;
        var log = await ValidaVeiculo(veiculo);

        if (log.Count > 0)
            throw new Exception("Veículo inválido: " + String.Join("; ", log));

        var veiculoModel = _mapper.Map<VeiculoViewModel, Veiculo>(veiculo);
        return await _veiculoRepository.CreateAsync(veiculoModel);
    }

    public async Task<VeiculoViewModel> UpdateAsync(VeiculoViewModel veiculo, int id)
    {
        var log = await ValidaVeiculo(veiculo);

        if (log.Count > 0)
            throw new Exception("Veículo inválido: " + String.Join("; ", log));

        var veiculoModel = _mapper.Map<VeiculoViewModel, Veiculo>(veiculo);
        var veiculoBanco = await _veiculoRepository.FindByIdAsync(id);

        if (veiculoBanco is null)
            throw new Exception($"Veículo de id {id} não encontrado");

        veiculoBanco.Placa = veiculoModel.Placa;

        var retorno = await _veiculoRepository.UpdateAsync(veiculoBanco);

        return _mapper.Map<Veiculo, VeiculoViewModel>(retorno); ;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var veiculoBanco = await GetVeiculoByIdAsync(id);

        if (veiculoBanco is null)
            throw new Exception($"Veículo de id {id} não encontrado");

        return await _veiculoRepository.DeleteAsync(id);
    }
}
