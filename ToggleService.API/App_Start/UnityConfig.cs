using Microsoft.Practices.Unity;
using ToggleService.API.Controllers;
using ToggleService.Aplication;
using ToggleService.DataMongoDB;
using ToggleService.DataMongoDB.Entities;
using ToggleService.DataMongoDB.Repository;


namespace ToggleService.API
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<AdminController>();
            container.RegisterType<ToggleRepository>(new InjectionConstructor());
            container.RegisterType<ToggleContext>(new InjectionConstructor());
            container.RegisterType<IToggleRepository, ToggleRepository>();
            container.RegisterType<IToggleApplication, ToggleApplication>();
            
            return container;
        }
    }
}