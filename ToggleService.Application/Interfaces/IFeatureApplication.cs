using System.Collections.Generic;
using ToggleService.Data.Entities;

namespace ToggleService.Application.Interfaces
{
    public interface IFeatureApplication
    {
        Feature GetFeature(string description);
        Feature GetFeature(int id);
        IEnumerable<Feature> GetAllFeature();
      
    }
}
