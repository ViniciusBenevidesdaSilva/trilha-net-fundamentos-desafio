using WebAPI.Domain.Model;

namespace WebAPI.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<IList<T>> FindAllAsync();
    Task<T> FindByIdAsync(int id);
    Task<int> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int  id);
}
