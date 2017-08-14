using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToggleService.API.Models
{
    public class FeatureModel
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int Version { get; set; }
    }
}