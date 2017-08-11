using System.Linq;
using ToggleService.Application.Interfaces;
using ToggleService.Data;
using ToggleService.Data.Interfaces;
using ToggleService.Domain;

namespace ToggleService.Application
{
    public class FeatureToggleApplication: IFeatureToggleApplication
    {
        private readonly IServiceRepository _repository;

        public FeatureToggleApplication(IServiceRepository repository)
        {
            _repository = repository;
        }
        
        public FeatureToggle EnableOrDisableFeature(int service, int idFeature, bool enabled)
        {
            var serviceData = _repository.Find(service);

            //if (serviceData == null)
                return null;

            //var findFeatureToggles = serviceData.FeaturesToggles.Where(x => x.Feature.Id == idFeature);
            //foreach (var toggle in findFeatureToggles)
            //{
            //    toggle.Enabled = enabled;
            //}


            //if (featureUpdate == null) return null;

            //featureUpdate. = enabled;
            //var featureEnableOrDisable = _repository.Update(featureUpdate);
            //return featureEnableOrDisable.Status;
        }
    }
}
