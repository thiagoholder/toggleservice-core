using System.Collections.Generic;
using System.Linq;
using ToggleService.Application.Interfaces;
using ToggleService.Data;
using ToggleService.Data.Interfaces;
using ToggleService.Domain;

namespace ToggleService.Application
{
    public class FeatureApplication: IFeatureApplication
    {
        private readonly IFeatureRepository _repository;

        public FeatureApplication(IFeatureRepository repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<Feature> GetAllFeature() => _repository.GetAll();
        public RepositoryActionResult<Feature> InsertFeature(Feature obj) => _repository.Insert(obj);
        public RepositoryActionResult<Feature> UpdateFeature(Feature obj) => _repository.Update(obj);
        public Feature GetFeature(string description) => _repository.Get(x => x.Description == description).FirstOrDefault();
        public Feature GetFeature(int id) => _repository.Find(id);
        
        
    }
}
