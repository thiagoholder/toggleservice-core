using Microsoft.Practices.Unity;
using ToggleService.Application;
using ToggleService.Application.Interfaces;
using ToggleService.API.Controllers;
using ToggleService.Data.Entities;
using ToggleService.Data.Interfaces;
using ToggleService.Data.Repositorys;

namespace ToggleService.API
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<AdminController>();
            
            container.RegisterType<IFeatureRepository, FeatureRepository>();
            container.RegisterType<IFeatureApplication, FeatureApplication>();
            container.RegisterType<IServiceRepository, ServiceRepository>();
            container.RegisterType<IServiceApplication, ServiceApplication>();
            container.RegisterType<FeatureContext>(new InjectionConstructor());
          
            return container;
        }
    }
}