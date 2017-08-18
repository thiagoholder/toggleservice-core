using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;
using OpenIddict.Models;
using ToggleService.Data.Repository.Interface;
using AutoMapper;
using ToggleService.Data.Entities;
using ToggleService.WebApi.Models;
using System.Linq;

namespace ToggleService.WebApi.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme, Roles = "Features")]
    [Produces("application/json")]
    [Route("api")]
    public class ResourceController : Controller
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _applicationManager;
        private readonly IToggleRepository _toggleRepository;
        private readonly IMapper _mapper;

        public ResourceController(OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
            IToggleRepository toggleRepository, IMapper mapper)
        {
            _applicationManager = applicationManager;
            _toggleRepository = toggleRepository;
            _mapper = mapper;

        }

        
        [HttpGet("features")]
        public async Task<IActionResult> GetFeatures()
        {
            try
            {
                var subject = User.FindFirst(OpenIdConnectConstants.Claims.Subject)?.Value;
                if (string.IsNullOrEmpty(subject))
                {
                    return BadRequest();
                }

                

                var application = await _applicationManager.FindByClientIdAsync(subject, HttpContext.RequestAborted);
               
                if (application == null)
                {
                    return BadRequest();
                }

                var toggle = await _toggleRepository.GetToggleByAppName(application.ClientId);
                if (toggle == null)
                    return StatusCode(401);

                var toggleModel = _mapper.Map<Toggle, ToggleModel>(toggle);

                return Ok(toggleModel);
            }
            catch (System.Exception)
            {

                return StatusCode(500);
            }
        }

        [Authorize(ActiveAuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        [HttpGet("features/{featureName}")]
        public async Task<IActionResult> GetFeature(string featureName)
        {
            try
            {
                var subject = User.FindFirst(OpenIdConnectConstants.Claims.Subject)?.Value;
                if (string.IsNullOrEmpty(subject))
                {
                    return BadRequest();
                }

                var application = await _applicationManager.FindByClientIdAsync(subject, HttpContext.RequestAborted);
                if (application == null)
                {
                    return BadRequest();
                }

                var toggle = await _toggleRepository.GetToggleByAppName(application.ClientId);
                if (toggle == null)
                    return StatusCode(401);

                var feature = toggle.Features.FirstOrDefault(x => x.Name == featureName);
                if (feature == null)
                    return NotFound();

                var featuretogglemodel = new FeatureToggleModel
                {
                    AppKey = toggle.AppName,
                    Enabled = feature.Enabled,
                    FeatureName = feature.Name,
                    Version = feature.Version
                };

                return Ok(featuretogglemodel);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
           
        }
        
    }
}