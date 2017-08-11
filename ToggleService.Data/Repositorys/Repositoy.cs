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
        private FeatureContext _context;
        
          public Repositoy(FeatureContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public TEntity Find(params object[] key)
        {
            return _context.Set<TEntity>().Find(key);
        }

        public RepositoryActionResult<TEntity> Update(TEntity obj)
        {
            try
            {
                var existingFeature = Find(obj.Id);

                if (existingFeature == null)
                {
                    return new RepositoryActionResult<TEntity>(obj, RepositoryActionStatus.NotFound);
                }
                _context.Entry(existingFeature).State = EntityState.Detached;
                _context.Set<TEntity>().Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;

                var result = _context.SaveChanges();
                return result > 0
                    ? new RepositoryActionResult<TEntity>(obj, RepositoryActionStatus.Updated)
                    : new RepositoryActionResult<TEntity>(obj, RepositoryActionStatus.NothingModified, null);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<TEntity>(null, RepositoryActionStatus.Error, ex);
            }
        }
  

        public RepositoryActionResult<TEntity> Insert(TEntity obj)
        {
            try
            {
                _context.Set<TEntity>().Add(obj);
                var result = _context.SaveChanges();
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
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
