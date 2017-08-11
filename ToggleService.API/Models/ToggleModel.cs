using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToggleService.API.Models
{
    public class ToggleModel
    {
        public FeatureModel Feature { get; set; }
        public bool Enabled { get; set; }

    }
}