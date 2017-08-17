using System.Threading.Tasks;
using ToggleService.Data.Entities;

namespace ToggleService.AppService.Interfaces
{
    public interface IToggleAppService
    {
        Task AddNewFeature(string serviceUniqueName, Feature feature);
        Task DeleteFeature(string serviceUniqueName, string featureName);
        Task UpdateFeature(string serviceUniqueName, Feature feature);
    }
}
