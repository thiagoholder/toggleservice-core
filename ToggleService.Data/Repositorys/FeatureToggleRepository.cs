using System;
using System.Data.Entity;
using ToggleService.Data.Entities;
using ToggleService.Data.Interfaces;
using ToggleService.Domain;

namespace ToggleService.Data.Repositorys
{
    public class FeatureToggleRepository: IFeatureToggleRepository
    {
        public FeatureContext Context { get; private set; }


        public FeatureToggleRepository(FeatureContext ctx)
        {
            Context = ctx;
        }
        
        public RepositoryActionResult<FeatureToggle> Update(int idFeature, int idService, bool enabled)
        {
            try
            {
                var existingFeature = Context.Toggles.Find(idFeature, idService);
                if (existingFeature == null)
                {
                    existingFeature = new FeatureToggle(){ IdFeature = idFeature, IdService = idService, Enabled = enabled};
                    return new RepositoryActionResult<FeatureToggle>(existingFeature, RepositoryActionStatus.NotFound);
                }
                existingFeature.Enabled = enabled;
                Context.Entry(existingFeature).State = EntityState.Detached;
                Context.Set<FeatureToggle>().Attach(existingFeature);
                Context.Entry(existingFeature).State = EntityState.Modified;

                var result = Context.SaveChanges();
                return result > 0
                    ? new RepositoryActionResult<FeatureToggle>(existingFeature, RepositoryActionStatus.Updated)
                    : new RepositoryActionResult<FeatureToggle>(existingFeature, RepositoryActionStatus.NothingModified, null);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }

        public RepositoryActionResult<FeatureToggle> Insert(int idFeature, int idService, bool enabled)
        {
            try
            {
                var newFeature = new FeatureToggle() { IdFeature = idFeature, IdService = idService, Enabled = enabled };
                Context.Set<FeatureToggle>().Add(newFeature);
                var result = Context.SaveChanges();
                return result > 0
                    ? new RepositoryActionResult<FeatureToggle>(newFeature, RepositoryActionStatus.Created)
                    : new RepositoryActionResult<FeatureToggle>(newFeature, RepositoryActionStatus.NothingModified, null);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<FeatureToggle>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
