using FluentAssertions;
using ToggleService.Data;
using ToggleService.Data.Repositorys;
using Xunit;

namespace ToggleService.IntegrationTests
{
    public class RepositoryTestes: SetupDBTests
    {
        private IFeatureRepository _featureRepository;


        [Fact]
        public void RepositorioFuncionando()
        {
            _featureRepository = new FeatureRepository(FeatureContext);
            _featureRepository.Should().NotBeNull();

            var features = _featureRepository.GetAll();
            features.Should().NotBeNull();

        }
    }
}
