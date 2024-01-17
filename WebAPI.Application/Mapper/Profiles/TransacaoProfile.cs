using AutoMapper;
using WebAPI.Application.ViewModel;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Mapper.Profiles;

public class TransacaoProfile : Profile
{
    public TransacaoProfile()
    {
        CreateMap<Transacao, TransacaoViewModel>();
        CreateMap<TransacaoViewModel, Transacao>();
    }
}
