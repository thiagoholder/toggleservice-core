using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using ToggleService.Application;
using ToggleService.Application.Interfaces;
using ToggleService.Data;
using ToggleService.Data.Entities;
using Xunit;

namespace ToggleService.UnitTests.Application
{
    public class FeatureTogglesTest
    {
        private readonly IFeatureApplication _feature;

        public FeatureTogglesTest()
        {
            IList<Feature> listFeatures = new List<Feature>
            {
                new Feature { Description = "Blue", Id = 1, Version = 1, Type = "Button"},
                new Feature { Description = "Red", Id = 2, Version = 1, Type = "Button" },
                new Feature { Description = "Yellow",  Id = 3, Version = 1, Type = "Button" }
            };

            var mockRepository = new Mock<IFeatureRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(listFeatures.AsQueryable());
            _feature = new FeatureApplication(mockRepository.Object);
        }

        //[Fact]
        //public void Existing_Feature_toggle_is_true_when_on()
        //{
        //    _feature.Enabled("Blue").Should().BeTrue();
        //}

        //[Fact]
        //public void Existing_Feature_toggle_is_false_when_off()
        //{
        //    _feature.Enabled("Red").Should().BeFalse();
        //}

        //[Fact]
        //public void Unconfiguraed_Feature_toggle_is_always_false()
        //{
        //    _feature.Enabled("OtherFeature").Should().BeFalse();
        //}

    }
}
