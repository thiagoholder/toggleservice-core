using ToggleService.Data;
using ToggleService.Domain;

namespace ToggleService.Application.Interfaces
{
    public interface IFeatureToggleApplication
    {
        FeatureToggle EnableOrDisableFeature(int service, int idFeature, bool enabled);
    }
}
