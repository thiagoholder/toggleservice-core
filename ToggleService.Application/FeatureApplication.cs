using System.Collections.Generic;
using System.Linq;
using ToggleService.Application.Interfaces;
using ToggleService.Data;
using ToggleService.Data.Entities;

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
     
        public Feature GetFeature(string description) => _repository.Get(x => x.Description == description).FirstOrDefault();
        public Feature GetFeature(int id) => _repository.Get(x => x.Id == id).FirstOrDefault();
        
    }
}
