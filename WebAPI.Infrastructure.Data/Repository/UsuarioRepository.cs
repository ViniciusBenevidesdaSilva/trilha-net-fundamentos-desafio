using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Model;
using WebAPI.Infrastructure.Data.Data;

namespace WebAPI.Infrastructure.Data.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EstacionamentoDbContext _context;

    public UsuarioRepository(EstacionamentoDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Usuario>> FindAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> FindByIdAsync(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Usuario> FindByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());
    }

    public async Task<int> CreateAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return usuario.Id;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        if(usuario is null)
            throw new ArgumentNullException(nameof(usuario), "O objeto usuário estava vazio");

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await FindByIdAsync(id);

        if (usuario is null)
            throw new Exception($"Usuário de Id {id} não encontrado");

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return true;
    }
}
