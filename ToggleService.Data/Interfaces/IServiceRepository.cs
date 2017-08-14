using System.Linq;
using ToggleService.Domain;

namespace ToggleService.Data.Interfaces
{
    public interface IServiceRepository: IRepository<Service>
    {
        IQueryable<Service> GetAllWithToggles();
    }
}
