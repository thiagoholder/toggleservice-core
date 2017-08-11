using System.Collections.Generic;

namespace ToggleService.Domain
{
    public class Service : Entity
    {

        public string Name { get; set; }

        public virtual ICollection<FeatureToggle> Toggles { get; set; }
    }
}
