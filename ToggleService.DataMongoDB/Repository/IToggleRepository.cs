using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ToggleService.DataMongoDB.Entities;

namespace ToggleService.DataMongoDB.Repository
{
    public interface IToggleRepository
    {
        Task<IEnumerable<Toggle>> GetAllToggles();
        Task<Toggle> GetToggle(string id);
        Task AddToggle(Toggle item);
        Task<DeleteResult> RemoveToggle(string id);
        Task<ReplaceOneResult> UpdateToggleDocument(string id, Toggle itemToggle);
        Task<Toggle> GetToggleByAppName(string appName);
    }
}
