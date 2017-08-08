using System;
using System.Linq;
using System.Linq.Expressions;
using ToggleService.Data.Entities;

namespace ToggleService.Data
{
    public interface IFeatureRepository
    {
        IQueryable<Feature> GetAllFeatures();
        IQueryable<Feature> Find(Expression<Func<Feature, bool>> predicate);
        Feature GetFeature(int id);

        Feature GetFeature(string description);

        RepositoryActionResult<Feature> InsertFeature(Feature feature);
        RepositoryActionResult<Feature> UpdateExpense(Feature feature);
    }
}
