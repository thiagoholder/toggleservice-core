using System;
using System.Data.Entity;
using ToggleService.Data.Entities;

namespace ToggleService.IntegrationTests
{
    public class SetupDBTests: IDisposable
    {
        public FeatureContext TestFeatureContext { get;  }

        public SetupDBTests()
        {
            Database.SetInitializer(new TestInitializer());
            TestFeatureContext = new FeatureContext("name = testDataBase");
            TestFeatureContext.Database.Initialize(true);
        }
        
        public void Dispose()
        {
            TestFeatureContext.Database.Delete();
            TestFeatureContext?.Dispose();
        }
    }
}
