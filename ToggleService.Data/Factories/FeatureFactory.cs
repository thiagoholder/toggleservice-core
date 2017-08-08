using ToggleService.Data.Entities;

namespace ToggleService.Data.Factories
{
    public class FeatureFactory
    {
        public DTO.Feature CreateFeature(Feature feature)
        {
            return new DTO.Feature
            {
                Id = feature.IdFeature,
                Description = feature.Description,
                Enabled = feature.Enabled,
                Version = feature.Version
            };
        }

        public Feature CreateFeature(DTO.Feature feature)
        {
            return new Feature
            {
                IdFeature = feature.Id,
                Description = feature.Description,
                Enabled = feature.Enabled,
                Version = feature.Version
            };
        }
    }
}
