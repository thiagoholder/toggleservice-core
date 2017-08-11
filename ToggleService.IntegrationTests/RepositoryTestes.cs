using System.Linq;
using FluentAssertions;
using ToggleService.Builders;
using ToggleService.Data;
using ToggleService.Data.Repositorys;
using ToggleService.Domain;
using Xunit;

namespace ToggleService.IntegrationTests
{
    public class RepositoryTestes : SetupDBTests
    {
        [Fact]
        public void Save_Entity_In_DataBase_Using_Generic_Repository()
        {
            var feature = new FeatureBuilder()
                .WithDescription("New Feature")
                .WithType("New Type")
                .WithVersion(1)
                .Build();

            var genericRepository = new Repositoy<Feature>(TestFeatureContext);
            var result = genericRepository.Insert(feature);

            genericRepository.Should().NotBeNull();
            result.Status.Should().Be(RepositoryActionStatus.Created);
        }

        [Fact]
        public void Get_All_Existing_Entities_From_DataBase_Using_Generic_Repository()
        {
            var genericRepository = new Repositoy<Feature>(TestFeatureContext);
            var result = genericRepository.GetAll();

            result.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void Find_A_Unique_Entitie_From_DataBase_Using_Generic_Repository()
        {
            var feature = new FeatureBuilder()
                .WithDescription("New Feature C")
                .WithType("New Type C")
                .WithVersion(1)
                .Build();

            var genericRepository = new Repositoy<Feature>(TestFeatureContext);
            var repositoryResult = genericRepository.Insert(feature);
            var result = genericRepository.Find(repositoryResult.Entity.Id);

            repositoryResult.Status.Should().Be(RepositoryActionStatus.Created);
            result.Should().NotBeNull();
            result.Id.Should().Be(repositoryResult.Entity.Id);
        }

        [Fact]
        public void Search_Entities_From_DataBase_Using_Generic_Repository_With_Predicate_Search()
        {
            var feature = new FeatureBuilder()
                .WithDescription("New Feature A")
                .WithType("New Type A")
                .WithVersion(1)
                .Build();

            var genericRepository = new Repositoy<Feature>(TestFeatureContext);
            var repositoryResult = genericRepository.Insert(feature);
            var result = genericRepository.Get(x => x.Description.Contains("New Feature A"));

            repositoryResult.Status.Should().Be(RepositoryActionStatus.Created);
            result.Should().HaveCount(1).And.Contain(repositoryResult.Entity);
        }
        [Fact]
        public void Update_Entities_From_DataBase_Using_Generic_Repository()
        {
            var feature = new FeatureBuilder()
                .WithDescription("New Feature AB")
                .WithType("New Type AB")
                .WithVersion(1)
                .Build();

            var genericRepository = new Repositoy<Feature>(TestFeatureContext);
            var repositoryResult = genericRepository.Insert(feature);
            var entitieUpdate = repositoryResult.Entity;
            entitieUpdate.Description = "New Feature Update";
            repositoryResult = genericRepository.Update(entitieUpdate);

            repositoryResult.Status.Should().Be(RepositoryActionStatus.Updated);
            genericRepository.Find(entitieUpdate.Id).Should().NotBeNull();

        }

    }
}
