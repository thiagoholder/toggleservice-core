using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToggleService.AppService.Interfaces;
using ToggleService.Data.Entities;
using ToggleService.Data.Repository.Interface;
using ToggleService.WebApi.Models;

namespace ToggleService.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/administrator")]
    public class AdministratorController : Controller
    {
        private readonly IToggleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IToggleAppService _toggleAppService;

        public AdministratorController(IToggleRepository repository, IMapper mapper, IToggleAppService toggleAppService)
        {
            _repository = repository;
            _mapper = mapper;
            _toggleAppService = toggleAppService;
        }

        [Route("toggles")]
        [HttpGet]
        public async Task<IActionResult> GetAllToggles()
        {
            try
            {
                var togglesFound = await _repository.GetAllToggles();
                var enumerable = togglesFound as IList<Toggle> ?? togglesFound.ToList();

                if (!enumerable.Any())
                    return NotFound();

                var toggleModel = _mapper.Map<List<Toggle>, List<ToggleModel>>(enumerable.ToList());
                return Ok(toggleModel);
            }
            catch (Exception)
            {
                return StatusCode(500);
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
        [HttpGet]
        public async Task<IActionResult> GetToggle(string uniqueServiceKey)
        {
            try
            {
                var toggle = await _repository.GetToggle(uniqueServiceKey);
                if (toggle == null)
                    return NotFound();
                var toggleModel = _mapper.Map<Toggle, ToggleModel>(toggle);
                return Ok(toggleModel);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get One Feature 
        /// </summary>
        /// <param name="uniqueServiceKey">Unique Id Toggle</param>
        /// <param name="nameFeature">Name Of Feature</param>
        /// <returns></returns>
        [Route("toggles/{uniqueServiceKey}/{nameFeature}", Name = "ToggleFeature")]
        [HttpGet]
        public async Task<IActionResult> GetToggleFeature(string uniqueServiceKey, string nameFeature)
        {
            try
            {
                var toggle = await _repository.GetToggle(uniqueServiceKey);
                if (toggle == null)
                    return NotFound();

                toggle.Features = toggle.Features.Where(x => x.Name == nameFeature).ToList();
                if (!toggle.Features.Any())
                    return NotFound();
                
                var featureModel = new FeatureToggleModel
                {
                    AppKey = toggle.AppName,
                    FeatureName = toggle.Features.Select(x => x.Name).FirstOrDefault(),
                    Enabled = toggle.Features.Select(x => x.Enabled).FirstOrDefault(),
                    Version = toggle.Features.Select(x => x.Version).FirstOrDefault()
                };
                
                return Ok(featureModel);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Add New Feature to Toggle
        /// </summary>
        /// <param name="uniqueServiceKey">Unique Id Toggle</param>
        /// <param name="featureItem"></param>
        /// <returns></returns>
        [Route("toggles/{uniqueServiceKey}/feature")]
        [HttpPost]
        public async Task<IActionResult> PostNewFeature(string uniqueServiceKey, [FromBody] FeatureModel featureItem)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest();
               
                var feature = _mapper.Map<FeatureModel, Feature>(featureItem);
                await _toggleAppService.AddNewFeature(uniqueServiceKey, feature);               
               
                return Created($"toggles/{uniqueServiceKey}/{featureItem.Name}", featureItem);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update Feature Toggle
        /// </summary>
        /// <param name="uniqueServiceKey">Unique Id Toggle</param>
        /// <param name="featureItem"></param>
        /// <returns></returns>
        [Route("toggles/{uniqueServiceKey}/feature")]
        [HttpPut]
        public async Task<IActionResult> UpdateFeature(string uniqueServiceKey, [FromBody] FeatureModel featureItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var feature = _mapper.Map<FeatureModel, Feature>(featureItem);
                await _toggleAppService.UpdateFeature(uniqueServiceKey, feature);

                return Accepted(featureItem);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update Feature Toggle
        /// </summary>
        /// <param name="uniqueServiceKey">Unique Id Toggle</param>
        /// <param name="featureName">Name Feature</param>
        /// <returns></returns>
        [Route("toggles/{uniqueServiceKey}/feature")]
        [HttpPut]
        public async Task<IActionResult> DeleteFeature(string uniqueServiceKey, string featureName)
        {
            try
            {
                await _toggleAppService.DeleteFeature(uniqueServiceKey, featureName);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


    }
}