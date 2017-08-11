using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToggleService.Domain;

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

            var defaultServices = new List<Service>()
            {
                new Service {Name = "Service A", Id = 1},
                new Service {Name = "Service B", Id = 2 },
                new Service {Name = "Service C", Id = 3}
            };
            foreach (var service in defaultServices)
            {
                context.Services.Add(service);
            }

            var toggleFeaturesA = new List<FeatureToggle>()
            {
                new FeatureToggle()
                {
                    Enabled = false,
                    Feature = defaultFeatures.Find(x => x.Id == 3),
                    Service = defaultServices.Find(x => x.Id == 1 )
                }
            };



            base.Seed(context);
        }
    }
}
