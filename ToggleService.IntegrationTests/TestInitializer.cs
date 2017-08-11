using System.Data.Entity;
using System.Data.Entity.Migrations;
using ToggleService.Data.Entities;
using ToggleService.Domain;

namespace ToggleService.IntegrationTests
{
    public class TestInitializer : DropCreateDatabaseAlways<FeatureContext>
    {
        protected override void Seed(FeatureContext context)
        {
            context.Features.AddOrUpdate(
                new Feature {Description = "Button Green", Version = 1, Type = "Button"},
                new Feature {Description = "Button Yellow",  Version = 1, Type = "Button"},
                new Feature {Description = "Button Blue",  Version = 1, Type = "Button"}
            );
            base.Seed(context);
        }
    }
}
