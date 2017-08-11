using System.Collections.Generic;
using ToggleService.Data;
using ToggleService.Domain;

namespace ToggleService.Application.Interfaces
{
    public interface IServiceApplication
    {

        Service GetService(string description);
        Service GetService(int id);
        IEnumerable<Service> GetAllServices();
        RepositoryActionResult<Service> InsertService(Service obj);
        RepositoryActionResult<Service> UpdateService(Service obj);
        
    }
}
