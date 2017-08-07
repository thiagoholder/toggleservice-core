using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
