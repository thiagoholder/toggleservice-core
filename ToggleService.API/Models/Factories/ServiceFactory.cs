using System.Linq;
using ToggleService.Domain;

namespace ToggleService.API.Models
{
    public class ServiceFactory
    {
        public ServiceModel CretaService(Service service)
        {
            return new ServiceModel
            {
                Id = service.Id,
                Name = service.Name
            };
        }

        public Service CretaService(ServiceModel service)
        {
            return new Service
            {
                Id = service.Id,
                Name = service.Name
            };
        }

        public ServiceDetailsModel CretaServiceWithDetail(Service service)
        {
            var serviceModel = new ServiceDetailsModel
            {
                Id = service.Id,
                Name = service.Name,
            };

            if (!service.Toggles.Any()) return serviceModel;

            serviceModel.Toggles = (from featureToggle in service.Toggles
                                    let feature = featureToggle.Feature
                                    select new ToggleModel
                                    {
                                        Enabled = featureToggle.Enabled,
                                        Feature = new FeatureModel()
                                        {
                                            Description = feature.Description,
                                            Id = feature.Id,
                                            Version = feature.Version
                                        }
                                    }).ToList();

            return serviceModel;
        }
    }
}