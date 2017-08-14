using System.Collections.Generic;
using System.Threading.Tasks;
using ToggleService.DataMongoDB.Entities;
using ToggleService.DataMongoDB.Repository;

namespace ToggleService.Aplication
{
    public class ToggleApplication: IToggleApplication
    {
        private readonly IToggleRepository _toggleRepository;

        public ToggleApplication(IToggleRepository toggleRepository)
        {
            _toggleRepository = toggleRepository;
        }


        public async Task<IEnumerable<Toggle>> GetAllToggles() =>  await _toggleRepository.GetAllToggles();
       

        public async Task<Toggle> GetToggle(string id) => await _toggleRepository.GetToggle(id);
       

        public async Task AddToggle(Toggle item) => await _toggleRepository.AddToggle(item);

        public async Task<bool> RemoveToggle(string id)
        {
           var result = await _toggleRepository.RemoveToggle(id);
           return result.IsAcknowledged;

        }

        public async Task<bool> UpdateToggleDocument(string id, Toggle itemToggle)
        {
            var result = await _toggleRepository.UpdateToggleDocument(id, itemToggle);
            return result.IsAcknowledged;
        }

        public async Task AddNewFeatureToggle(string id, Feature feature)
        {
            var updatedToggle = await GetToggle(id);
            updatedToggle?.AddFeature(feature);
        }
    }
}
