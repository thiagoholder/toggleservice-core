using ToggleService.Data.Entities;
using ToggleService.Data.Interfaces;
using ToggleService.Domain;

namespace ToggleService.Data.Repositorys
{
    public class ServiceRepository: Repositoy<Service>, IServiceRepository
    {
        public ServiceRepository(FeatureContext ctx) : base(ctx)
        {
        }
    }
}
