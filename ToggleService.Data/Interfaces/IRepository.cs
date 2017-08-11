using System;
using System.Linq;

namespace ToggleService.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
        TEntity Find(params object[] key);
        RepositoryActionResult<TEntity> Update(TEntity obj);
        RepositoryActionResult<TEntity> Insert(TEntity obj);
        RepositoryActionResult<TEntity> Delete(Func<TEntity, bool> predicate);
    }
}
