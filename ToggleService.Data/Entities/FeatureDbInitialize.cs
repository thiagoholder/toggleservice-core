using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleService.Data.Entities
{
    public class FeatureDbInitialize : CreateDatabaseIfNotExists<FeatureContext>
    {
        protected override void Seed(FeatureContext context)
        {
            var defaultFeatures = new List<Feature>()
            { new Feature { Description = "Button Green", Enabled = true, Version = 1},
              new Feature { Description = "Button Yellow", Enabled = false, Version = 1 },
              new Feature { Description = "Button Blue", Enabled = true, Version = 1 }
            };
            foreach (var std in defaultFeatures)
                context.Features.Add(std);

            base.Seed(context);
        }
    }
}
