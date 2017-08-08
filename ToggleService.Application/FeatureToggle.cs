using System.Collections.Generic;
using System.Linq;
using ToggleService.Data;
using ToggleService.Data.Entities;
using ToggleService.Data.Repositorys;

namespace ToggleService.Application
{
    public class FeatureToggle: IFeatureToggle
    {
        private readonly IFeatureRepository _repository;

        public FeatureToggle()
        {
            _repository = new FeatureRepository(new
                FeatureContext());
        }

        public FeatureToggle(IFeatureRepository repository)
        {
            _repository = repository;
        }

        public bool Disabled(string feature)
        {
            return !Enabled(feature);

        }

        public bool Enabled(string feature)
        {
            return FeatureToggles().ContainsKey(feature) && FeatureToggles()[feature];
        }

        public IEnumerable<Feature> GetAllFeature()
        {
            return _repository.GetAllFeatures();
        }

        public IEnumerable<Feature> GetAllEnabledFeatures()
        {
            return _repository.Find(x => x.Enabled);
        }

        private Dictionary<string, bool> FeatureToggles()
        {
            var result = new Dictionary<string, bool>();
            var featureToggles = GetAllFeature().ToList();

            if (!featureToggles.Any()) return result;
            
            foreach (var feature in featureToggles)
            {
                result.Add(feature.Description, feature.Enabled);
            }
            return result;
        }
    }
}
