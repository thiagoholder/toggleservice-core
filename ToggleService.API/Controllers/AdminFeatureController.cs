using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ToggleService.Application.Interfaces;
using ToggleService.Data.Entities;
using ToggleService.Data.Factories;

namespace ToggleService.API.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminFeatureController : ApiController
    {
        private readonly IFeatureApplication _toogleApplication;
        private readonly FeatureFactory _featureFactory = new FeatureFactory();

        public AdminFeatureController(IFeatureApplication application)
        {
            _toogleApplication = application;
        }

        /// <summary>
        /// Get all enabled features from de central database.
        /// </summary>
        /// <returns>All Enabled Features</returns>
        /// <response code="404" >Not found enabled features</response>
        [Route("features/enableds")]
        [ResponseType(typeof(IEnumerable<DTO.Feature>))]
        [HttpGet]
        public IHttpActionResult GetAllEnabledFeatures()
        {
            try
            {
                var featuresEnabled = _toogleApplication.GetAllFeature().ToList();
                return featuresEnabled.Any() ? (IHttpActionResult) Ok(ListFeaturesDto(featuresEnabled)) : NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get all disabled features from de central database.
        /// </summary>
        /// <returns>All Disabled Features</returns>
        /// <response code="404" >Not found disabled features</response>
        [Route("features/disabled")]
        [ResponseType(typeof(IEnumerable<DTO.Feature>))]
        [HttpGet]
        public IHttpActionResult GetAllDisabledFeatures()
        {
            try
            {
                var featuresEnabled = _toogleApplication.GetAllFeature();
                return Ok(ListFeaturesDto(featuresEnabled));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        /// <summary>
        /// Get all features from de central database.
        /// </summary>
        /// <returns>All Features</returns>
        /// <response code="404">Not found features</response>
        [Route("features")]
        [ResponseType(typeof(IEnumerable<DTO.Feature>))]
        [HttpGet]
        public IHttpActionResult GetAllFeatures()
        {
            try
            {
                var features = _toogleApplication.GetAllFeature();
                return Ok(ListFeaturesDto(features));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        private IEnumerable<DTO.Feature> ListFeaturesDto(IEnumerable<Feature> featuresEnabled)
        {
            return featuresEnabled.Select(f => _featureFactory.CreateFeature(f));
        }
    }
}
