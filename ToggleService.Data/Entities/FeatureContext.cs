using System.Data.Entity;
using ToggleService.Data.Maps;

namespace ToggleService.Data.Entities
{
    public class FeatureContext : DbContext
    {
        public FeatureContext(string nameOrConnectionString) : base(nameOrConnectionString){}

        public FeatureContext() : base("name = FeatureContext")
        {
            Database.SetInitializer(new FeatureDbInitialize());
        }

        public DbSet<Feature> Features { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FeatureMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
