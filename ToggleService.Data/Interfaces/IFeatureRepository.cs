using System.Collections.Generic;
using ToggleService.Data.Entities;

namespace ToggleService.Data
{
    public interface IFeatureRepository
    {
        IEnumerable<Feature> GetAllFeatures();
        IEnumerable<Feature> GetAllEnabledFeatures();
        Feature GetFeature(int id);

        Feature GetFeature(string description);
    }
}
