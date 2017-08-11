using System.Collections.Generic;
using System.Linq;
using ToggleService.Application.Interfaces;
using ToggleService.Data;
using ToggleService.Data.Interfaces;
using ToggleService.Domain;

namespace ToggleService.Application
{
    public class ServiceApplication: IServiceApplication
    {
        private readonly IServiceRepository _repository;

        public ServiceApplication(IServiceRepository repository)
        {
            _repository = repository;
        }
        public Service GetService(string name) => _repository.Get(x => x.Name == name).FirstOrDefault();
        public Service GetService(int id) => _repository.Find(id);
        public IEnumerable<Service> GetAllServices() => _repository.GetAll();
        public RepositoryActionResult<Service> InsertService(Service obj) => _repository.Insert(obj);
        public RepositoryActionResult<Service> UpdateService(Service obj) => _repository.Update(obj);
    }
}
