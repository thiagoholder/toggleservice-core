using System.Data.Entity;
using System.Data.Entity.Migrations;
using ToggleService.Data.Entities;

namespace ToggleService.IntegrationTests
{
    public class TestInitializer : DropCreateDatabaseAlways<FeatureContext>
    {
        protected override void Seed(FeatureContext context)
        {
            context.Features.AddOrUpdate(
                new Feature {Description = "Button Green", Enabled = true, Version = 1, Type = "Button"},
                new Feature {Description = "Button Yellow", Enabled = false, Version = 1, Type = "Button"},
                new Feature {Description = "Button Blue", Enabled = true, Version = 1, Type = "Button"}
            );
            base.Seed(context);
        }
    }
}
