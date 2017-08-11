using ToggleService.Data;

namespace ToggleService.Application.Interfaces
{
    public interface IFeatureToggleApplication
    {
        RepositoryActionStatus EnableOrDisableFeature(int idFeature, bool enabled);
    }
}
