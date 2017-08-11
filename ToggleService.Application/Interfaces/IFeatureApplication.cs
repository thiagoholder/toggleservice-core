using System.Collections.Generic;
using ToggleService.Data;
using ToggleService.Domain;

namespace ToggleService.Application.Interfaces
{
    public interface IFeatureApplication
    {
        Feature GetFeature(string description);
        Feature GetFeature(int id);
        IEnumerable<Feature> GetAllFeature();
        RepositoryActionResult<Feature> InsertFeature(Feature obj);
        RepositoryActionResult<Feature> UpdateFeature(Feature obj);
    }
}
