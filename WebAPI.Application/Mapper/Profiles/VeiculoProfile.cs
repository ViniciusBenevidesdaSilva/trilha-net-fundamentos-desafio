using AutoMapper;
using WebAPI.Application.ViewModel;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Mapper.Profiles;

public class VeiculoProfile : Profile
{
    public VeiculoProfile()
    {
        CreateMap<Veiculo, VeiculoViewModel>();
        CreateMap<VeiculoViewModel, Veiculo>();
    }
}
