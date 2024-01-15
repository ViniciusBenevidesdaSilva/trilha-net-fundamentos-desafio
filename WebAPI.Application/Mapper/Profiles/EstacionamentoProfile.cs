using AutoMapper;
using WebAPI.Application.ViewModel;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Mapper.Profiles;

public class EstacionamentoProfile : Profile
{
    public EstacionamentoProfile()
    {
        CreateMap<Estacionamento, EstacionamentoViewModel>();
        CreateMap<EstacionamentoViewModel, Estacionamento>();
    }
}
