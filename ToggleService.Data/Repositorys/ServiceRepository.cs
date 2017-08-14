using System.Linq;
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

        public IQueryable<Service> GetAllWithToggles()
        {
            Context.Toggles
                .Include("Toggles")
                .Include("Toggles.Feature");
            return Context.Set<Service>();
        }

        public override Service Find(params object[] key)
        {
            Context.Toggles
                .Include("Toggles")
                .Include("Toggles.Feature");

            return base.Find(key);
        }
    }
}
