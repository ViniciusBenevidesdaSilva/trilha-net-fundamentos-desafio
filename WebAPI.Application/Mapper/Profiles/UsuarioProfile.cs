using AutoMapper;
using WebAPI.Application.ViewModel;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Mapper.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioViewModel>();
        CreateMap<UsuarioViewModel, Usuario>();

    }
}
