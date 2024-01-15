using WebAPI.Application.ViewModel;

namespace WebAPI.Application.Interfaces;

public interface IUsuarioService
{
    Task<IList<UsuarioViewModel>> GetUsuariosAsync();
    Task<UsuarioViewModel> GetUsuarioByIdAsync(int id);
    Task<UsuarioViewModel> GetUsuarioByEmailAsync(string email);
    Task<int> CreateAsync(UsuarioViewModel usuario);
    Task<UsuarioViewModel> UpdateAsync(UsuarioViewModel usuario, int id);
    Task<bool> DeleteAsync(int id);
    Task<UsuarioViewModel?> AutenticaUsuario(UsuarioViewModel usuario);
}
