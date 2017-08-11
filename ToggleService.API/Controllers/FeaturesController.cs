using System;
using System.Web.Http;
using ToggleService.Application;
using ToggleService.Application.Interfaces;
using ToggleService.Data.Entities;
using ToggleService.Data.Factories;
using ToggleService.Data.Repositorys;

namespace ToggleService.API.Controllers
{
    [RoutePrefix("api/feature")]
    public class FeaturesController : ApiController
    {
        private readonly IFeatureApplication _toogleApplication;
        private readonly FeatureFactory _featureFactory = new FeatureFactory();

        public FeaturesController()
        {
            _toogleApplication = new FeatureApplication(new FeatureRepository(new FeatureContext()));
        }

        public FeaturesController(IFeatureApplication toogleApplication)
        {
            _toogleApplication = toogleApplication;
        }
        /// <summary>
        /// Get a feature by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Feature is found</returns>
        /// <response code="404">If feature item is notfound</response>
        [Route("{id:int}")]
        public IHttpActionResult GetFeatureById(int id)
        {
            try
            {
                var featureFound = _toogleApplication.GetFeature(id);
                return featureFound != null ? (IHttpActionResult) Ok(featureFound) : NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get a feature by id
        /// </summary>
        /// <param name="description"></param>
        /// <returns>Feature id found</returns>
        /// <response code="404">If feature item is notfound</response>
        [Route("{description}")]
        public IHttpActionResult GetFeatureByDescription(string description)
        {
            try
            {
                var featureFound = _toogleApplication.GetFeature(description);
                return featureFound != null ? (IHttpActionResult)Ok(featureFound) : NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
