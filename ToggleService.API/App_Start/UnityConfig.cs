using System.Data.Entity;
using Microsoft.Practices.Unity;
using ToggleService.Application;
using ToggleService.Application.Interfaces;
using ToggleService.API.Controllers;
using ToggleService.Data;
using ToggleService.Data.Entities;
using ToggleService.Data.Repositorys;

namespace ToggleService.API
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<AdminFeatureController>();


            container.RegisterType<IFeatureRepository, FeatureRepository>();
            container.RegisterType<IFeatureApplication, FeatureApplication>();
            container.RegisterType<FeatureContext>(new InjectionConstructor());
          


            return container;
        }
    }
}