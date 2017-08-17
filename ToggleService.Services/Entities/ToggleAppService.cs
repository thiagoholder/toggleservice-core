using System.Linq;
using System.Threading.Tasks;
using ToggleService.AppService.Interfaces;
using ToggleService.Data.Entities;
using ToggleService.Data.Repository.Interface;

namespace ToggleService.AppService.Entities
{
    public class ToggleAppService: IToggleAppService
    {
        private readonly IToggleRepository _toggleRepository;
        public ToggleAppService(IToggleRepository toggleRepository)
        {
            _toggleRepository = toggleRepository;
        }

        public async Task AddNewFeature(string serviceUniqueName, Feature feature)
        {
            var updatedToggle = await _toggleRepository.GetToggle(serviceUniqueName);
            if (updatedToggle != null)
            {
                updatedToggle.AddFeature(feature);
                await _toggleRepository.UpdateToggleDocument(serviceUniqueName, updatedToggle);
            }
        }

        public async Task DeleteFeature(string serviceUniqueName, string featureName)
        {
            var updatedToggle = await _toggleRepository.GetToggle(serviceUniqueName);
            if (updatedToggle != null)
            {
                var featureDelete = updatedToggle.Features.FirstOrDefault(x => x.Name == featureName);
                updatedToggle.RemoveFeature(featureDelete);
                await _toggleRepository.UpdateToggleDocument(serviceUniqueName, updatedToggle);
            }
        }

        public async Task UpdateFeature(string serviceUniqueName, Feature feature)
        {
            var updatedToggle = await _toggleRepository.GetToggle(serviceUniqueName);
            if (updatedToggle != null)
            {
                var featureUpdate = updatedToggle.Features.FirstOrDefault(x => x.Name == feature.Name);
                updatedToggle.RemoveFeature(featureUpdate);
                updatedToggle.AddFeature(featureUpdate);
                await _toggleRepository.UpdateToggleDocument(serviceUniqueName, updatedToggle);
            }
        }
    }
}
