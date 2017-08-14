using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToggleService.API.Models.Factories
{
    public class ToggleFeatureModel
    {
        public string AppKey { get; set; }
        public string FeatureName { get; set; }
        public int Version { get; set; }
        public bool Enabled { get; set; }
    }
}