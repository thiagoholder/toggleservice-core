using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ToggleService.Builders;
using ToggleService.DataMongoDB;
using ToggleService.DataMongoDB.Repository;
using Xunit;

namespace ToggleService.IntegrationTests
{
    public class RepositoryTestes : SetupDBTests
    {
        private readonly IToggleRepository _repository;

        public RepositoryTestes()
        {
            _repository = new ToggleRepository();
        }

        [Fact]
        public async Task Save_Toggle_In_DataBase_And_Return_New_Entitty()
        {
            var toggle = new ToggleBuilder().WithAppName("New Toggle")
                .Build();
            await _repository.AddToggle(toggle);
            
            toggle.AppName.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_All_Toggles()
        {
            var allToggles = await _repository.GetAllToggles();
            allToggles.Count().Should().BeGreaterThan(0);
        }



        [Fact]
        public async Task Find_A_Unique_Toggle()
        {
            var toggle = new ToggleBuilder().WithAppName("Teste Find")
                .Build();
            await _repository.AddToggle(toggle);
            var toggleFind = await _repository.GetToggle(toggle.AppName);
            toggleFind.Should().NotBeNull();
        }

        [Fact]
        public async Task Remove_toggle_by_id()
        {
            var toggle = new ToggleBuilder().WithAppName("Teste")
                .Build();

            await _repository.AddToggle(toggle);

            var countExistingToggle = _repository.GetAllToggles().Result.Count();
            var delete = await _repository.RemoveToggle(toggle.AppName);
            var allTogglesBeforeDelete = await _repository.GetAllToggles();

            delete.DeletedCount.Should().Be(1);

            allTogglesBeforeDelete.Should().HaveCount(countExistingToggle - 1);

        }
        [Fact]
        public async Task Update_Toggle_by_item()
        {
            const string newName = "Updated Toogle";
            var toggle = new ToggleBuilder().WithAppName("ToggleNew")
                .Build();
            await _repository.AddToggle(toggle);
            toggle.AppName = newName;
            await _repository.UpdateToggleDocument(toggle.AppName, toggle);

            var findUpdateToggle = await _repository.GetToggle(toggle.AppName);
            findUpdateToggle.AppName.Should().Be(newName);
        }

        [Fact]
        public async Task Get_Toggle_by_Name()
        {
           var toggle = new ToggleBuilder().WithAppName("Toggle EveryWhere")
                .Build();
            await _repository.AddToggle(toggle);
            var findUpdateToggle = await _repository.GetToggleByAppName(toggle.AppName);
            findUpdateToggle.Should().NotBeNull();
        }

    }
}
