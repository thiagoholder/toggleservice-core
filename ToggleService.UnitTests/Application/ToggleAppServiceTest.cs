using System.Linq;
using System.Threading.Tasks;
using AutoMoq;
using FluentAssertions;
using MongoDB.Driver;
using Moq;
using ToggleService.AppService.Entities;
using ToggleService.Builder;
using ToggleService.Data.Entities;
using ToggleService.Data.Repository.Interface;
using Xunit;

namespace ToggleService.UnitTests.Application
{
    public class ToggleAppServiceTest
    {

        [Fact]
        public async Task When_calling_the_application_service_should_able_add_new_feature()
        {
            var toggleFeature = new ToggleBuilder().WithAppName("ServiceTeste")
                .WithFeatures(new Feature() { Enabled = true, Name = "Feature A", Version = 2 }).Build();
            var newFeature = new Feature() {Enabled = true, Name = "Feature B", Version = 2};
            var mocker = new AutoMoqer();
            var moqRepository = mocker.GetMock<IToggleRepository>();
            moqRepository.Setup(x => x.GetToggle(It.IsAny<string>()))
                .ReturnsAsync(toggleFeature);
            moqRepository.Setup(x => x.UpdateToggleDocument(It.IsAny<string>(), toggleFeature))
                .ReturnsAsync((ReplaceOneResult) null);

            toggleFeature.Features.Should().HaveCount(2);

            var toggleAppService = new ToggleAppService(moqRepository.Object);
            await toggleAppService.AddNewFeature(toggleFeature.AppName, newFeature);

            toggleFeature.Features.Should().HaveCount(3);
            moqRepository.Verify(x => x.GetToggle(It.IsAny<string>()), Times.Once);
            moqRepository.Verify(x => x.UpdateToggleDocument(It.IsAny<string>(), toggleFeature), Times.Once);
        }

        [Fact]
        public async Task When_calling_the_application_service_should_able_delete_a_feature()
        {
          
            var newFeature = new Feature() { Enabled = true, Name = "Feature B", Version = 2 };

            var toggleFeature = new ToggleBuilder().WithAppName("ServiceTeste")
                .WithFeatures(new Feature() { Enabled = true, Name = "Feature A", Version = 2 })
                .WithFeatures(newFeature)
                .Build();

            var mocker = new AutoMoqer();
            var moqRepository = mocker.GetMock<IToggleRepository>();
            moqRepository.Setup(x => x.GetToggle(It.IsAny<string>()))
                .ReturnsAsync(toggleFeature);
            moqRepository.Setup(x => x.UpdateToggleDocument(It.IsAny<string>(), toggleFeature)).ReturnsAsync((ReplaceOneResult)null); ;

            toggleFeature.Features.Should().HaveCount(3);

            var toggleAppService = new ToggleAppService(moqRepository.Object);
            await toggleAppService.DeleteFeature(toggleFeature.AppName, newFeature.Name);

            toggleFeature.Features.Should().HaveCount(2);
            moqRepository.Verify(x => x.GetToggle(It.IsAny<string>()), Times.Once);
            moqRepository.Verify(x => x.UpdateToggleDocument(It.IsAny<string>(), toggleFeature), Times.Once);
        }

        [Fact]
        public async Task When_calling_the_application_service_should_able_update_a_feature()
        {

           
            var toggleFeature = new ToggleBuilder().WithAppName("ServiceTeste")
                .WithFeatures(new Feature() { Enabled = true, Name = "Feature A", Version = 2 })
                .Build();
            var featureUpdated = toggleFeature.Features.Find(x => x.Name == "Feature A");

            featureUpdated.Name = "Feature A Update";

            var mocker = new AutoMoqer();
            var moqRepository = mocker.GetMock<IToggleRepository>();
            moqRepository.Setup(x => x.GetToggle(It.IsAny<string>()))
                .ReturnsAsync(toggleFeature);
            moqRepository.Setup(x => x.UpdateToggleDocument(It.IsAny<string>(), toggleFeature)).ReturnsAsync((ReplaceOneResult)null); ;
            
            var toggleAppService = new ToggleAppService(moqRepository.Object);
            await toggleAppService.UpdateFeature(toggleFeature.AppName, featureUpdated);

            toggleFeature.Features.Should().HaveCount(2);
            toggleFeature.Features.Select(x => x.Name == featureUpdated.Name).Should().NotBeNull();
            moqRepository.Verify(x => x.GetToggle(It.IsAny<string>()), Times.Once);
            moqRepository.Verify(x => x.UpdateToggleDocument(It.IsAny<string>(), toggleFeature), Times.Once);

        }
    }
}
