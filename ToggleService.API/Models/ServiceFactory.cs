using ToggleService.Domain;

namespace ToggleService.API.Models
{
    public class ServiceFactory
    {
        public ServiceModel CretaService(Service service)
        {
            return new ServiceModel
            {
                Id = service.Id,
                Name = service.Name
            };
        }

        public Service CretaService(ServiceModel service)
        {
            return new Service
            {
                Id = service.Id,
                Name = service.Name
            };
        }
    }
}