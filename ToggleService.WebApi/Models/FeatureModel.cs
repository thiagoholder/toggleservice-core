using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToggleService.WebApi.Models
{
    public class FeatureModel
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int Version { get; set; }
    }
}
