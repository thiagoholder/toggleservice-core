using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ToggleService.Data.Entities;

namespace ToggleService.IntegrationTest.DBTestContext
{
    public class DBContextTest: IToggleContext
    {
        private readonly IMongoDatabase _database = null;

        public DBContextTest(IConfiguration config)
        {
            var connection = config.GetConnectionString("ToggleContext");
            var mongoUrl = new MongoUrl(connection);
            var client = new MongoClient(mongoUrl);
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<Toggle> Toggles { get; }
    }
}
