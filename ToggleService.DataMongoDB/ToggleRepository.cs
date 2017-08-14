using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ToggleService.DataMongoDB.Entities;
using ToggleService.DataMongoDB.Repository;

namespace ToggleService.DataMongoDB
{
    public class ToggleRepository : IToggleRepository
    {
        private readonly ToggleContext _context = null;

        public ToggleRepository(string connection) => _context = new ToggleContext(connection);

        public ToggleRepository() => _context = new ToggleContext();

        public async Task<IEnumerable<Toggle>> GetAllToggles()
        {
            return await _context.Toggles.Find(_ => true).ToListAsync();
        }

        public async Task<Toggle> GetToggle(string appname)
        {
            var filter = Builders<Toggle>.Filter.Eq(x => x.AppName, appname);
            return await _context.Toggles
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<Toggle> GetToggleByAppName(string appName)
        {
            var filter = Builders<Toggle>.Filter.Eq(x => x.AppName, appName);
            
            return await _context.Toggles
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task AddToggle(Toggle item)
        {
            if (await GetToggleByAppName(item.AppName) != null)
            {
                await RemoveToggle(item.AppName);
            }
            await _context.Toggles.InsertOneAsync(item);
        }

        public async Task<DeleteResult> RemoveToggle(string appname)
        {
            return await _context.Toggles.DeleteOneAsync(
                Builders<Toggle>.Filter.Eq(x => x.AppName, appname));
        }

        public async Task<ReplaceOneResult> UpdateToggleDocument(string appname, Toggle itemToggle)
        {
            return await _context.Toggles
                .ReplaceOneAsync(n => n.AppName.Equals(appname)
                    , itemToggle
                    , new UpdateOptions { IsUpsert = true });
        }

        
    }
}
