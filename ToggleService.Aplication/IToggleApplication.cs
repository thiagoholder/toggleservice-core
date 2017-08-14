using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleService.DataMongoDB.Entities;

namespace ToggleService.Aplication
{
    public interface IToggleApplication
    {
        Task<IEnumerable<Toggle>> GetAllToggles();
        Task<Toggle> GetToggle(string id);
        Task AddToggle(Toggle item);
        Task<bool> RemoveToggle(string id);
        Task<bool> UpdateToggleDocument(string id, Toggle itemToggle);
        Task AddNewFeatureToggle(string id, Feature feature);

    }
}
