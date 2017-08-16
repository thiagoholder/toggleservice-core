using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ToggleService.Data.Entities
{
    public class ToggleContext: IToggleContext
    {
        private readonly IMongoDatabase _database = null;

        public ToggleContext(IConfiguration config)
        {
            var connection = config.GetConnectionString("ToggleContext");
            var mongoUrl = new MongoUrl(connection);
            var client = new MongoClient(mongoUrl);
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<Toggle> Toggles => _database.GetCollection<Toggle>("Toggle");

    }
}
