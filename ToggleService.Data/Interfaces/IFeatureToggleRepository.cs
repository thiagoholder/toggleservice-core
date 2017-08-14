using ToggleService.Domain;

namespace ToggleService.Data.Interfaces
{
    public interface IFeatureToggleRepository
    {
        RepositoryActionResult<FeatureToggle> Update(int idFeature, int idService, bool enabled);
        RepositoryActionResult<FeatureToggle> Insert(int idFeature, int idService, bool enabled);
    }
}
