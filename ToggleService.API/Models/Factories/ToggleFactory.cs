using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using ToggleService.API.Models.Factories;
using ToggleService.DataMongoDB.Entities;

namespace ToggleService.API.Models
{
    public class ToogleFactory
    {
        public ToggleModel CreateToggle(Toggle toggle)
        {
            return new ToggleModel
            {
                AppKey = toggle.AppName,
                Features = toggle.Features
            };
        }

        public Toggle CreateToggle(ToggleModel toggleModel)
        {
            return new Toggle
            {
                AppName = toggleModel.AppKey,
               Features = toggleModel.Features
            };
        }

        public ToggleFeatureModel CreateToggleFeature(Toggle toggle)
        {
            return new ToggleFeatureModel
            {
                AppKey = toggle.AppName,
                Enabled = toggle.Features.Select(x => x.Enabled).FirstOrDefault(),
                Version = toggle.Features.Select(x => x.Version).FirstOrDefault(),
                FeatureName = toggle.Features.Select(x => x.Name).FirstOrDefault()
            };
        }

        public object CreateDataShapedObject(Toggle toggle, List<string> listOfFields)
        {

            return CreateDataShapedObject(CreateToggle(toggle), listOfFields);
        }

        public object CreateDataShapedObject(ToggleModel toggleModel, List<string> listOfFields)
        {
            if (!listOfFields.Any()) return toggleModel;

            var objectToReturn = new ExpandoObject();
            foreach (var field in listOfFields)
            {
                var fieldValue = toggleModel.GetType()
                    .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(toggleModel, null);

                ((IDictionary<string, object>) objectToReturn).Add(field, fieldValue);
            }

            return objectToReturn;
        }
    }
}