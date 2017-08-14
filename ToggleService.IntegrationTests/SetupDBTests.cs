using System;
using MongoDB.Bson;
using ToggleService.DataMongoDB.Entities;

namespace ToggleService.IntegrationTests
{
    public class SetupDBTests
    {
        public ToggleContext TestFeatureContext { get; }

        public SetupDBTests()
        {
           TestFeatureContext = new ToggleContext("ToggleContext");
        }

    }
}
