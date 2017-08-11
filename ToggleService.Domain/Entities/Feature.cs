﻿using System.Collections.Generic;

namespace ToggleService.Domain
{
    public class Feature: Entity
    {
        public string Description { get; set; }
        public int Version { get; set; }
        public string Type { get; set; }
        public ICollection<FeatureToggle> Toggles { get; set; }
    }
}
