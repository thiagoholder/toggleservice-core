using ToggleService.Domain;

namespace ToggleService.API.Models
{
    public class FeatureFactory
    {
        public FeatureModel CreateFeature(Feature feature)
        {
            return new FeatureModel
            {
                Id = feature.Id,
                Description = feature.Description,
                Version = feature.Version
            };
        }

        public Feature CreateFeature(FeatureModel feature)
        {
            return new Feature
            {
                Id = feature.Id,
                Description = feature.Description,
                Version = feature.Version
            };
        }
    }
}