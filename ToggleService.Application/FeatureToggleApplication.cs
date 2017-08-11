using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleService.Application.Interfaces;
using ToggleService.Data;

namespace ToggleService.Application
{
    public class FeatureToggleApplication: IFeatureToggleApplication
    {
        private readonly IFeatureRepository _repository;

        public FeatureToggleApplication(IFeatureRepository repository)
        {
            _repository = repository;
        }


        public RepositoryActionStatus EnableOrDisableFeature(int idFeature, bool enabled)
        {
            var featureUpdate = _repository.Find(idFeature);

            if (featureUpdate == null) return RepositoryActionStatus.Error;

            //featureUpdate.Enabled = enabled;
            var featureEnableOrDisable = _repository.Update(featureUpdate);
            return featureEnableOrDisable.Status;
        }
    }
}
