using FluentAssertions;
using ToggleService.Builder;
using ToggleService.Data.Entities;
using Xunit;

namespace ToggleService.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Toggle_add_new_Feature()
        {

            var toggle =
                new ToggleBuilder().WithFeatures(new Feature() { Enabled = true, Name = "Feature A", Version = 1 }).Build();

            var feature = new Feature() { Name = "Feature B", Enabled = false, Version = 3 };
            toggle.AddFeature(feature);

            toggle.Should().BeOfType<Toggle>();
            toggle.Should().NotBeNull();

            toggle.Features.Should().HaveCount(2);
            toggle.Features.Should().Contain(x => x == feature);
            toggle.Features.Should().Contain(x => x.Name == "Feature A");
        }

        [Fact]
        public void Should_Toggle_Remove_Existing_Feature()
        {

            var feature = new Feature() { Enabled = true, Name = "Feature A", Version = 1 };
            var toggle =
                new ToggleBuilder().WithFeatures(feature).Build();
            toggle.Features.Should().HaveCount(1);

            toggle.RemoveFeature(feature);
            toggle.Features.Should().HaveCount(0);
        }

        [Fact]
        public void Should_Toggle_Find_Existing_Feature()
        {
            var featureA = new Feature() { Enabled = true, Name = "Feature A", Version = 1 };
            var featureB = new Feature() { Enabled = true, Name = "Feature B", Version = 1 };
            var toggle =
                new ToggleBuilder().WithFeatures(featureA).Build();
            toggle.AddFeature(featureB);

            var findFeature = toggle.FindFeature(x => x.Name == featureA.Name);

            toggle.Features.Should().HaveCount(2);
            findFeature.Should().BeOfType<Feature>();
            findFeature.Should().NotBeNull();
            findFeature.ShouldBeEquivalentTo(featureA);
        }


        [Fact]
        public void Should_Not_Add_The_Same_Feature()
        {
            var featureA = new Feature() { Enabled = true, Name = "Feature A", Version = 1 };
            var featureB = new Feature() { Enabled = true, Name = "Feature B", Version = 1 };
            var toggle = new ToggleBuilder().WithFeatures(featureA).Build();

            toggle.AddFeature(featureB);
            toggle.AddFeature(featureA);

            toggle.Features.Should().HaveCount(2);

        }
        [Fact]
        public void Should_Not_Remove_Inexisting_Feature()
        {
            var featureA = new Feature() { Enabled = true, Name = "Feature A", Version = 1 };
            var toggle = new ToggleBuilder().WithFeatures(featureA).Build();
            toggle.Features.Should().HaveCount(1);

            var featureB = new Feature() { Enabled = true, Name = "Feature B", Version = 1 };
            toggle.RemoveFeature(featureB);

            toggle.Features.Should().HaveCount(1);
            toggle.Features.Should().NotContain(x => x == featureB);
        }
    }
}
