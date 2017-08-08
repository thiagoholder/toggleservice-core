using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ToggleService.Data.Entities;

namespace ToggleService.Data.Repositorys
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly FeatureContext _context;

        public FeatureRepository(FeatureContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Feature> GetAllFeatures()
        {
            return _context.Features;
        }

        public IQueryable<Feature> Find(Expression<Func<Feature, bool>> predicate)
        {
          return GetAllFeatures().Where(predicate);
        }

        public Feature GetFeature(int id)
        {
            return _context.Features.Find(id);
        }

        public Feature GetFeature(string description)
        {
            return _context.Features.FirstOrDefault(x => x.Description.ToLower().Contains(description.ToLower()));
        }

        public RepositoryActionResult<Feature> InsertFeature(Feature feature)
        {
            try
            {
                _context.Features.Add(feature);
                var result = _context.SaveChanges();
                return result > 0 
                    ? new RepositoryActionResult<Feature>(feature, RepositoryActionStatus.Created) 
                    : new RepositoryActionResult<Feature>(feature, RepositoryActionStatus.NothingModified, null);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Feature>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Feature> UpdateExpense(Feature feature)
        {
            try
            {
                var existingFeature = _context.Features.FirstOrDefault(exp => exp.IdFeature == feature.IdFeature);

                if (existingFeature == null)
                {
                    return new RepositoryActionResult<Feature>(feature, RepositoryActionStatus.NotFound);
                }
                _context.Entry(existingFeature).State = EntityState.Detached;
                _context.Features.Attach(feature);
                _context.Entry(feature).State = EntityState.Modified;

                var result = _context.SaveChanges();
                return result > 0
                    ? new RepositoryActionResult<Feature>(feature, RepositoryActionStatus.Updated)
                    : new RepositoryActionResult<Feature>(feature, RepositoryActionStatus.NothingModified, null);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Feature>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
