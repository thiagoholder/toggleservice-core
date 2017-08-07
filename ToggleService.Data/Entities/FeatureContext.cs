using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleService.Data.Maps;

namespace ToggleService.Data.Entities
{
    public class FeatureContext: DbContext
    {
        public FeatureContext(): base("name=FeatureContext")
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
