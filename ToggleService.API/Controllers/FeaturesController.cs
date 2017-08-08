using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ToggleService.Data;
using ToggleService.Data.Entities;
using ToggleService.Data.Factories;
using ToggleService.Data.Repositorys;

namespace ToggleService.API.Controllers
{
    [RoutePrefix("api")]
    public class FeaturesController : ApiController
    {
        private readonly IFeatureRepository _repository;
        private readonly FeatureFactory _featureFactory = new FeatureFactory();
     
        public FeaturesController()
        {
            _repository = new FeatureRepository(new
                FeatureContext());
        }

        public FeaturesController(IFeatureRepository repository)
        {
            _repository = repository;
        }

        [Route("features/enabled")]
        [ResponseType(typeof(IEnumerable<DTO.Feature>))]
        public IHttpActionResult GetAllEnabledFeatures()
        {
            try
            {
                var featuresEnabled = _repository.GetAllFeatures();
                return Ok(ListFeaturesDto(featuresEnabled));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("features")]
        [ResponseType(typeof(IEnumerable<DTO.Feature>))]
        public IHttpActionResult GetAllFeatures()
        {
            try
            {
                var features = _repository.GetAllFeatures();
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

        [Route("features/{id:int}")]
        [ResponseType(typeof(DTO.Feature))]
        public IHttpActionResult GetFeature(int id)
        {
            try
            {
                var features = _repository.GetFeature(id);
                return features == null ? (IHttpActionResult) NotFound() : Ok(_featureFactory.CreateFeature(features));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("features/{description}")]
        [ResponseType(typeof(DTO.Feature))]
        public IHttpActionResult GetFeatureByDescription(string description)
        {
            try
            {
                var features = _repository.GetFeature(description);
                return features == null ? (IHttpActionResult)NotFound() : Ok(_featureFactory.CreateFeature(features));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] DTO.Feature feature)
        {
            try
            {
                if (feature == null)
                {
                    return BadRequest();
                }

                var featureEntity = _featureFactory.CreateFeature(feature);
                var result = _repository.InsertFeature(featureEntity);

                if (result.Status != RepositoryActionStatus.Created) return BadRequest();

                var newFeature = _featureFactory.CreateFeature(result.Entity);
                return Created(Request.RequestUri + "/" + newFeature.Id, newFeature);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
