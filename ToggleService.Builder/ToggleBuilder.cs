﻿using System.Collections.Generic;
using ToggleService.Data.Entities;

namespace ToggleService.Builder
{
    public class ToggleBuilder
    {
        private string _appName = "Default Toggle";

        private readonly List<Feature> _features = new List<Feature>()
        {
            new Feature(){ Enabled = true, Name = "Feature Testes", Version = 1 }
        };

        public Toggle Build()
        {
            return new Toggle { AppName = _appName, Features = _features };
        }

        public ToggleBuilder WithAppName(string appName)
        {
            this._appName = appName;
            return this;
        }

        public ToggleBuilder WithFeatures(Feature feature)
        {
            _features.Add(feature);
            return this;
        }

        public static implicit operator Toggle(ToggleBuilder instance)
        {
            return instance.Build();
        }
    }
}
