using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ToggleService.Data.Entities
{
    public class FeatureDbInitialize : CreateDatabaseIfNotExists<FeatureContext>
    {
        protected override void Seed(FeatureContext context)
        {
            var defaultFeatures = new List<Feature>()
            { new Feature { Description = "Button Green", Version = 1, Type = "Button", Id = 1},
              new Feature { Description = "Button Yellow", Version = 1, Type = "Button", Id = 2},
              new Feature { Description = "Button Blue", Version = 1, Type = "Button", Id = 3}
            };
            foreach (var std in defaultFeatures)
                context.Features.Add(std);
            var toggleFeatures = new List<FeatureToggle>()
            {
                new FeatureToggle()
                {
                    Enabled = true,
                    Feature = defaultFeatures.Find(x => x.Id == 1),
                },
                new FeatureToggle()
                {
                    Enabled = false,
                    Feature = defaultFeatures.Find(x => x.Id == 2),
                }
            };

            var toggleFeaturesA = new List<FeatureToggle>()
            {
                new FeatureToggle()
                {
                    Enabled = false,
                    Feature = defaultFeatures.Find(x => x.Id == 3),
                }
            };

            var defaultServices = new List<Service>()
            {
                new Service {Name = "Service A", FeaturesToggles = toggleFeaturesA.ToList()},
                new Service {Name = "Service B", FeaturesToggles = toggleFeatures.ToList()},
                new Service {Name = "Service C" }
            };
            foreach (var service in defaultServices)
            {
                context.Services.Add(service);
            }
            
            base.Seed(context);
        }
    }
}
