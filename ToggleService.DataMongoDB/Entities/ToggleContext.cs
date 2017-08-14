using System.Configuration;
using MongoDB.Driver;

namespace ToggleService.DataMongoDB.Entities
{
    public class ToggleContext
    {
        private readonly IMongoDatabase _database = null;

        public ToggleContext()
            : this("ToggleContext")
        {
        }


        public ToggleContext(string connectionName)
        {
            var url = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            var mongoUrl = new MongoUrl(url);
            var client = new MongoClient(mongoUrl);
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<Toggle> Toggles => _database.GetCollection<Toggle>("Toggle");

    }
}
