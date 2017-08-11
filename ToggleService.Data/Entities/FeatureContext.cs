﻿using System.Data.Entity;
using ToggleService.Data.Maps;

namespace ToggleService.Data.Entities
{
    public class FeatureContext : DbContext
    {
        public FeatureContext(string nameOrConnectionString) : base(nameOrConnectionString){}

        public FeatureContext() : base("Name=FeatureContext")
        {
            Database.SetInitializer(new FeatureDbInitialize());
        }

        public DbSet<Feature> Features { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<FeatureToggle> Toggles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FeatureMap());
            modelBuilder.Configurations.Add(new ServiceMap());
            modelBuilder.Configurations.Add(new FeatureToggleMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
