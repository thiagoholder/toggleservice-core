using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using ToggleService.Application;
using ToggleService.Data;
using ToggleService.Data.Entities;
using Xunit;

namespace ToggleService.UnitTests.Application
{
    public class FeatureTogglesTest
    {
        private readonly IFeatureToggle _featureToggle;

        public FeatureTogglesTest()
        {
            IList<Feature> listFeatures = new List<Feature>
            {
                new Feature { Description = "Blue", Enabled = true, IdFeature = 1, Version = 1, Type = "Button"},
                new Feature { Description = "Red", Enabled = false, IdFeature = 2, Version = 1, Type = "Button" },
                new Feature { Description = "Yellow", Enabled = true, IdFeature = 3, Version = 1, Type = "Button" }
            };

            var mockRepository = new Mock<IFeatureRepository>();
            mockRepository.Setup(x => x.GetAllFeatures()).Returns(listFeatures.AsQueryable());
            _featureToggle = new FeatureToggle(mockRepository.Object);
        }

        [Fact]
        public void Existing_Feature_toggle_is_true_when_on()
        {
            _featureToggle.Enabled("Blue").Should().BeTrue();
        }

        [Fact]
        public void Existing_Feature_toggle_is_false_when_off()
        {
            _featureToggle.Enabled("Red").Should().BeFalse();
        }

        [Fact]
        public void Unconfiguraed_Feature_toggle_is_always_false()
        {
            _featureToggle.Enabled("OtherFeature").Should().BeFalse();
        }

    }
}
