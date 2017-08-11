using System;
using System.Data.Entity;
using System.Linq;
using ToggleService.Data.Entities;
using ToggleService.Data.Interfaces;
using ToggleService.Domain;

namespace ToggleService.Data.Repositorys
{
    public class Repositoy<TEntity> : IDisposable,
        IRepository<TEntity> where TEntity : Entity
    {
        public FeatureContext Context { get; private set; }


        public Repositoy(FeatureContext ctx)
        {
            Context = ctx;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public virtual TEntity Find(params object[] key)
        {
            return Context.Set<TEntity>().Find(key);
        }

        public virtual RepositoryActionResult<TEntity> Update(TEntity obj)
        {
            try
            {
                var existingFeature = Find(obj.Id);

                if (existingFeature == null)
                {
                    return new RepositoryActionResult<TEntity>(obj, RepositoryActionStatus.NotFound);
                }
                Context.Entry(existingFeature).State = EntityState.Detached;
                Context.Set<TEntity>().Attach(obj);
                Context.Entry(obj).State = EntityState.Modified;

                var result = Context.SaveChanges();
                return result > 0
                    ? new RepositoryActionResult<TEntity>(obj, RepositoryActionStatus.Updated)
                    : new RepositoryActionResult<TEntity>(obj, RepositoryActionStatus.NothingModified, null);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
            }
        }
  

        public virtual RepositoryActionResult<TEntity> Insert(TEntity obj)
        {
            try
            {
                Context.Set<TEntity>().Add(obj);
                var result = Context.SaveChanges();
                return result > 0
                    ? new RepositoryActionResult<TEntity>(obj, RepositoryActionStatus.Created)
                    : new RepositoryActionResult<TEntity>(obj, RepositoryActionStatus.NothingModified, null);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<TEntity> Delete(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Context == null) return;
            Context.Dispose();
            Context = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
