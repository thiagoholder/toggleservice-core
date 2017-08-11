using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleService.Data.Interfaces;

namespace ToggleService.Data.Entities
{
    public class FeatureToggle: Entity
    {
        public int IdFeature { get; set; }
        public int IdService { get; set; }
        public Feature Feature { get; set; }
        public Service Service { get; set; }
        public bool Enabled { get; set; }
    }
}
