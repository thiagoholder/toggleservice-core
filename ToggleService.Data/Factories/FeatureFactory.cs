using ToggleService.Data.Entities;

namespace ToggleService.Data.Factories
{
    public class FeatureFactory
    {
        public DTO.Feature CreateFeature(Feature feature)
        {
            return new DTO.Feature
            {
                Id = feature.Id,
                Description = feature.Description,
                Version = feature.Version
            };
        }

        public Feature CreateFeature(DTO.Feature feature)
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
