using AutoMapper;
using WebAPI.Application.Interfaces;
using WebAPI.Application.ViewModel;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Model;
using WebAPI.Domain.Utils;

namespace WebAPI.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<IList<UsuarioViewModel>> GetUsuariosAsync()
    {
        var usuarios = await _usuarioRepository.FindAllAsync();
        return _mapper.Map<IList<Usuario>, IList<UsuarioViewModel>>(usuarios);
    }

    public async Task<UsuarioViewModel> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.FindByIdAsync(id);
        return _mapper.Map<Usuario, UsuarioViewModel>(usuario);
    }

    public async Task<UsuarioViewModel> GetUsuarioByEmailAsync(string email)
    {
        var usuario = await _usuarioRepository.FindByEmailAsync(email);
        return _mapper.Map<Usuario, UsuarioViewModel>(usuario);
    }

    private async Task<List<string>> ValidaUsuario(UsuarioViewModel usuario)
    {
        var retorno = new List<string>();

        if (usuario is null)
        {
            retorno.Add("Usuário não pode ser nulo");
            return retorno;
        }

        if(usuario.Id < 0)
            retorno.Add("O Id do usuário deve ser maior que 0");

        if(!Regex.ValidaFormatoEmail(usuario.Email))
            retorno.Add("Email inválido");

        var usuarioEmail = await GetUsuarioByEmailAsync(usuario.Email);

        if(usuarioEmail is not null)
        {
            if (usuario.Id == 0 || usuario.Id != usuarioEmail.Id)
                retorno.Add("Email já cadastrado");
        }

        if (usuario.Senha.Length < 3)
            retorno.Add("A senha deve possuir ao menos 3 caracteres");

        return retorno;
    }

    public async Task<int> CreateAsync(UsuarioViewModel usuario)
    {
        usuario.Id = 0;
        var log = await ValidaUsuario(usuario);

        if (log.Count > 0)
            throw new Exception("Usuário inválido: " + String.Join("; ", log));

        var usuarioModel = _mapper.Map<UsuarioViewModel, Usuario>(usuario);
        return await _usuarioRepository.CreateAsync(usuarioModel);
    }

    public async Task<UsuarioViewModel> UpdateAsync(UsuarioViewModel usuario, int id)
    {
        var log = await ValidaUsuario(usuario);

        if (log.Count > 0)
            throw new Exception("Usuário inválido: " + String.Join(" ;", log));

        var usuarioModel = _mapper.Map<UsuarioViewModel, Usuario>(usuario);
        var usuarioBanco = await _usuarioRepository.FindByIdAsync(id);

        if (usuarioBanco is null)
            throw new Exception($"Usuário de id {id} não encontrado");

        usuarioBanco.Nome = usuarioModel.Nome;
        usuarioBanco.Email = usuarioModel.Email;
        usuarioBanco.Senha = usuarioModel.Senha;

        var retorno = await _usuarioRepository.UpdateAsync(usuarioBanco);

        return _mapper.Map<Usuario, UsuarioViewModel>(retorno);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuarioBanco = await GetUsuarioByIdAsync(id);

        if (usuarioBanco is null)
            throw new Exception($"Usuário de id {id} não encontrado");

        return await _usuarioRepository.DeleteAsync(id);
    }

    public async Task<UsuarioViewModel?> AutenticaUsuario(UsuarioViewModel usuario)
    {
        if(usuario is null)
            throw new ArgumentNullException(nameof(usuario), "Usuário autenticado não pode ser nulo");

        if (usuario.Email.Length == 0 || usuario.Senha.Length == 0)
            return null;

        var usuarioBanco = await GetUsuarioByEmailAsync(usuario.Email);

        if (usuarioBanco is null || !usuarioBanco.Senha.Equals(usuario.Senha))
            return null;

        return usuarioBanco;
    }
}
