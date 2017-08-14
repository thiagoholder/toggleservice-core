using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ToggleService.API.Models;
using ToggleService.DataMongoDB.Entities;
using ToggleService.DataMongoDB.Repository;

namespace ToggleService.API.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {

        private readonly IToggleRepository _toggleRepository;
        private readonly ToogleFactory _toggleFactory = new ToogleFactory();

        public AdminController(IToggleRepository toggleRepository)
        {
            _toggleRepository = toggleRepository;
        }

        /// <summary>
        /// Get all features from de central database.
        /// </summary>
        /// <returns>All Features</returns>
        /// <response code="404">Not found toogles</response>
        [Route("toggles")]
        [ResponseType(typeof(IEnumerable<ToggleModel>))]
        [HttpGet]
        public async Task<IHttpActionResult>  GetAllToggles(string fields = null)
        {
            try
            {
                var listOfFields = new List<string>();
                if (fields != null)
                {
                    listOfFields = fields.ToLower().Split(',').ToList();
                   
                }

                var toggles = await _toggleRepository.GetAllToggles();
                var enumerable = toggles as IList<Toggle> ?? toggles.ToList();

                if (!enumerable.ToList().Any())
                    return NotFound();

                return Ok(enumerable.Select(t => _toggleFactory.CreateDataShapedObject(t, listOfFields)));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Receives a feature through id
        /// </summary>
        /// <param name="uniqueServiceKey">Unique Id</param>
        /// <returns>Toggle</returns>
        /// <response code="404">Not found toggles</response>
        /// <response code="200">Found toggles</response>
        [Route("toggles/{uniqueServiceKey}")]
        [ResponseType(typeof(ToggleModel))]
        [HttpGet]
        public async Task<IHttpActionResult>  GetToggle(string uniqueServiceKey, string fields = null)
        {
            try
            {
                var listOfFields = new List<string>();
                if (fields != null)
                {
                    listOfFields = fields.ToLower().Split(',').ToList();

                }

                var toggle = await _toggleRepository.GetToggle(uniqueServiceKey);
                return toggle == null
                    ? (IHttpActionResult)NotFound()
                    : Ok( _toggleFactory.CreateDataShapedObject(toggle, listOfFields));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get One Feature 
        /// </summary>
        /// <param name="uniqueServiceKey">Unique Id Toggle</param>
        /// <param name="nameFeature">Name Of Feature</param>
        /// <returns></returns>
        [Route("toggles/{uniqueServiceKey}/{nameFeature}")]
        [ResponseType(typeof(ToggleModel))]
        [HttpGet]
        public async Task<IHttpActionResult> GetToggleFeature(string uniqueServiceKey, string nameFeature)
        {
            try
            {
               
                var toggle = await _toggleRepository.GetToggle(uniqueServiceKey);
                if ( toggle == null)
                    return NotFound();


                toggle.Features = toggle.Features.Where(x => x.Name == nameFeature).ToList();
               
                return !toggle.Features.Any()
                    ? (IHttpActionResult)NotFound()
                    : Ok(_toggleFactory.CreateToggleFeature(toggle));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        /// <summary>
        /// Insert a new Toggle in System
        /// </summary>
        /// <param name="toggle"></param>
        /// <returns></returns>
        /// <response code="201">New Toggle Is Created</response>
        /// <response code="400">Toggle Is Not Created</response>
        [Route("toggles")]
        [ResponseType(typeof(ToggleModel))]
        [HttpPost]
        public async  Task<IHttpActionResult> PostToggle([FromBody] ToggleModel toggle)
        {
            try
            {
                if (toggle == null) return BadRequest();

                var newToggle = _toggleFactory.CreateToggle(toggle);
                await _toggleRepository.AddToggle(newToggle);
                
                var newToggleModel = _toggleFactory.CreateToggle(newToggle);
                return Created(Request.RequestUri + "/" + newToggle.AppName, newToggleModel);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Insert a new Toggle in System
        /// </summary>
        /// <param name="uniqueServiceKey">App Name</param>
        /// <returns></returns>
        /// <response code="201">New Toggle Is Created</response>
        /// <response code="400">Toggle Is Not Created</response>
        [Route("toggles")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteToggle(string uniqueServiceKey)
        {
            try
            {
                var toggle = _toggleRepository.GetToggle(uniqueServiceKey);
                await _toggleRepository.RemoveToggle(uniqueServiceKey);
                return Created(Request.RequestUri, toggle);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Insert a new Toggle in System
        /// </summary>
        /// <param name="uniqueServiceKey"></param>
        /// <param name="toggleModel">JSOn Toggle</param>
        /// <returns></returns>
        /// <response code="201">New Toggle Is Created</response>
        /// <response code="400">Toggle Is Not Created</response>
        [Route("toggles")]
        [HttpPut]
        public async Task<IHttpActionResult> PutToggle(string uniqueServiceKey, [FromBody] ToggleModel toggleModel)
        {
            try
            {
                if(string.IsNullOrEmpty(uniqueServiceKey) || toggleModel == null)
                    return BadRequest();

                var toggleExists = await _toggleRepository.GetToggle(uniqueServiceKey);

                if (toggleExists == null)
                    return NotFound();

                var updateToggle = _toggleFactory.CreateToggle(toggleModel);

                await _toggleRepository.UpdateToggleDocument(uniqueServiceKey, updateToggle);
                return Ok(updateToggle);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
