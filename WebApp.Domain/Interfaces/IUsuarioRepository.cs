using WebAPI.Domain.Model;

namespace WebAPI.Domain.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario> FindByEmailAsync(string email);
}
