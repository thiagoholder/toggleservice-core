using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToggleService.Data.Entities;

namespace ToggleService.IntegrationTests
{
    public class SetupDBTests: IDisposable
    {
        public FeatureContext FeatureContext { get;  }

        public SetupDBTests()
        {
            Database.SetInitializer(new TestInitializer());
            FeatureContext = new FeatureContext("name = testDataBase");
            FeatureContext.Database.Initialize(true);
        }
        
        public void Dispose()
        {
            FeatureContext.Database.Delete();
            FeatureContext?.Dispose();
        }
    }
}
