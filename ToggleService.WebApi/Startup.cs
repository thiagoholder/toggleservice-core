using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenIddict.Core;
using OpenIddict.Models;
using Swashbuckle.AspNetCore.Swagger;
using ToggleService.AppService.Entities;
using ToggleService.AppService.Interfaces;
using ToggleService.Data.Entities;
using ToggleService.Data.Repository;
using ToggleService.Data.Repository.Interface;
using ToggleService.WebApi.Models;

namespace ToggleService.WebApi
{
    public class Startup
    {
       

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

           

            var configMapper = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FeatureModel, Feature>();
                cfg.CreateMap<ToggleModel, Toggle>().ForMember(dest => dest.AppName, opt => opt.MapFrom(src => src.AppKey));

                cfg.CreateMap<Feature, FeatureModel>();
                cfg.CreateMap<Toggle, ToggleModel>().ForMember(dest => dest.AppKey, opt => opt.MapFrom(src => src.AppName));
            });

            var mapper = configMapper.CreateMapper();


            services.AddSingleton<IToggleRepository, ToggleRepository>();
            services.AddSingleton<IToggleContext, ToggleContext>();
            services.AddSingleton<IToggleAppService, ToggleAppService>();
            services.AddSingleton<IConfiguration>(_ => config);
            services.AddSingleton(mapper);
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented; 
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Toggle Rest API", Version = "v1" });
            });

            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Configure the context to use Microsoft SQL Server.
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            
            services.AddOpenIddict(options =>
            {
                // Register the Entity Framework stores.
                options.AddEntityFrameworkCoreStores<ApplicationDbContext>();

                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                options.AddMvcBinders();

                // Enable the token endpoint.
                options.EnableTokenEndpoint("/connect/token");

                // Enable the client credentials flow.
                options.AllowClientCredentialsFlow();

                // During development, you can disable the HTTPS requirement.
                options.DisableHttpsRequirement();

                // Note: to use JWT access tokens instead of the default
                // encrypted format, the following lines are required:
                //
                // options.UseJsonWebTokens();
                // options.AddEphemeralSigningKey();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseOAuthValidation();
            app.UseOpenIddict();
            app.UseMvcWithDefaultRoute();
            app.UseWelcomePage();
          InitializeAsync(app.ApplicationServices, CancellationToken.None).GetAwaiter().GetResult();

        }

        private async Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync();

                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                if (await manager.FindByClientIdAsync("console", cancellationToken) == null)
                {
                    var application = new OpenIddictApplication
                    {
                        ClientId = "console",
                        DisplayName = "My client application"
                    };

                    await manager.CreateAsync(application, "388D45FA-B36B-4988-BA59-B187D329C207", cancellationToken);
                }
                if (await manager.FindByClientIdAsync("consoleA", cancellationToken) == null)
                {
                    var application = new OpenIddictApplication
                    {
                        ClientId = "consoleA",
                        DisplayName = "My client application Console A"
                    };

                    await manager.CreateAsync(application, "388D45FA-B36B-4988-BA59-B187D329D207", cancellationToken);
                }

            }
        }
    }
}
