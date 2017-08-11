using System.Collections.Generic;
using ToggleService.Data.Interfaces;

namespace ToggleService.Data.Entities
{
    public class Service: Entity
    {
        public string Name { get; set; }
        public virtual IList<FeatureToggle> FeaturesToggles { get; set; }
    }
}
