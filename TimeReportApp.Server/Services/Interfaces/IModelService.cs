using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Services;

public interface IModelService<T,K>
{
    public Task<IEnumerable<T>> Get();
    public Task<T?> GetById(K id);
    public Task Put(K id, T entity);
    public Task<T?> Post(T entity);
    public Task Delete(K id);
    public bool Exists(K id);
}
