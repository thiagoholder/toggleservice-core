using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ToggleService.Application.Interfaces;
using ToggleService.API.Models;
using ToggleService.Data;
using ToggleService.Domain;

namespace ToggleService.API.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly IFeatureApplication _featureApplication;
        private readonly IServiceApplication _serviceApplication;
        private readonly FeatureFactory _featureFactory = new FeatureFactory();
        private readonly ServiceFactory _serviceFactory = new ServiceFactory();

        public AdminController(IFeatureApplication application, IServiceApplication serviceApplication)
        {
            _featureApplication = application;
            _serviceApplication = serviceApplication;
        }

        /// <summary>
        /// Get all features from de central database.
        /// </summary>
        /// <returns>All Features</returns>
        /// <response code="404">Not found features</response>
        [Route("features")]
        [ResponseType(typeof(IEnumerable<FeatureModel>))]
        [HttpGet]
        public IHttpActionResult GetAllFeatures()
        {
            try
            {
                var features = _featureApplication.GetAllFeature();
                return Ok(ListFeaturesDto(features));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Receives a feature through id
        /// </summary>
        /// <returns>Feature</returns>
        /// <param name="id">Unique Id</param>
        /// <response code="404">Not found feature</response>
        [Route("features/{id:int}")]
        [ResponseType(typeof(FeatureModel))]
        [HttpGet]
        public IHttpActionResult GetFeatureById(int id)
        {
            try
            {
                var feature = _featureApplication.GetFeature(id);
                return feature == null
                    ? (IHttpActionResult) NotFound()
                    : Ok(_featureFactory.CreateFeature(feature));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Receives a feature through description
        /// </summary>
        /// <returns>Feature</returns>
        /// <param name="description">Description Feature</param>
        /// <response code="404">Not found feature</response>
        [Route("features/{description}")]
        [ResponseType(typeof(FeatureModel))]
        [HttpGet]
        public IHttpActionResult GetFeatureByDescription(string description)
        {
            try
            {
                var feature = _featureApplication.GetFeature(description);
                return feature == null
                    ? (IHttpActionResult) NotFound()
                    : Ok(_featureFactory.CreateFeature(feature));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get all Services of the System
        /// </summary>
        /// <returns>Servicees</returns>
        /// <response code="404">Not found feature</response>
        [Route("services")]
        [ResponseType(typeof(IEnumerable<ServiceDetailsModel>))]
        [HttpGet]
        public IHttpActionResult GetAllServices()
        {
            try
            {
                var service = _serviceApplication.GetAllServices().ToList();
                return service.Any() ? (IHttpActionResult) Ok(ListServicesModel(service)) : NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }



        /// <summary>
        /// Receives a service through id
        /// </summary>
        /// <param name="id">Id Service</param>
        /// <returns>Service</returns>
        /// <response code="404">Not found feature</response>
        [Route("services/{id:int}")]
        [ResponseType(typeof(ServiceModel))]
        [HttpGet]
        public IHttpActionResult GetServiceById(int id)
        {
            try
            {
                var service = _serviceApplication.GetService(id);
                return service == null
                    ? (IHttpActionResult) NotFound()
                    : Ok(_serviceFactory.CretaService(service));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Receives a service through id
        /// </summary>
        /// <param name="name">Id Service</param>
        /// <returns>Service</returns>
        /// <response code="404">Not found feature</response>
        [Route("services/{name}")]
        [ResponseType(typeof(ServiceModel))]
        [HttpGet]
        public IHttpActionResult GetServiceByName(string name)
        {
            try
            {
                var service = _serviceApplication.GetService(name);
                return service == null
                    ? (IHttpActionResult) NotFound()
                    : Ok(_serviceFactory.CretaService(service));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Insert a new Feature in System
        /// </summary>
        /// <param name="feature">Feature</param>
        /// <returns></returns>
        /// <response code="201">New Feature Is Created</response>
        /// <response code="400">New Feature Is Not Created</response>
        [Route("features")]
        [ResponseType(typeof(DTO.Feature))]
        [HttpPost]
        public IHttpActionResult PostFeature([FromBody] FeatureModel feature)
        {
            try
            {
                if (feature == null) return BadRequest();

                var newFeature = _featureFactory.CreateFeature(feature);
                var result = _featureApplication.InsertFeature(newFeature);

                if (result.Status != RepositoryActionStatus.Created) return BadRequest();

                var newFeatureDto = _featureFactory.CreateFeature(result.Entity);
                return Created(Request.RequestUri + "/" + newFeatureDto.Id, newFeatureDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        /// <summary>
        /// Insert a new Service in the System
        /// </summary>
        /// <param name="service">Service</param>
        /// <returns></returns>
        /// <response code="201">New Service Is Created</response>
        /// <response code="400">New Service Is Not Created</response>
        [Route("services")]
        [ResponseType(typeof(ServiceModel))]
        [HttpPost]
        public IHttpActionResult PostService([FromBody] ServiceModel service)
        {
            try
            {
                if (service == null) return BadRequest();

                var newService = _serviceFactory.CretaService(service);
                var result = _serviceApplication.InsertService(newService);

                if (result.Status != RepositoryActionStatus.Created) return BadRequest();

                var newServiceDto = _serviceFactory.CretaService(result.Entity);
                return Created(Request.RequestUri + "/" + newServiceDto.Id, newServiceDto);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        private IEnumerable<FeatureModel> ListFeaturesDto(IEnumerable<Feature> features)
        {
            return features.Select(f => _featureFactory.CreateFeature(f));
        }

        private IEnumerable<ServiceDetailsModel> ListServicesModel(IEnumerable<Service> services)
        {
            return services.Select(s => _serviceFactory.CretaServiceWithDetail(s));
        }
    }
}
