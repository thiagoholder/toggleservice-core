using MongoDB.Driver;

namespace ToggleService.Data.Entities
{
    public interface IToggleContext
    {
        IMongoCollection<Toggle> Toggles { get;  }
    }
}