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
using Microsoft.AspNetCore.Identity;

namespace ToggleService.WebApi.Controllers
{

    [Authorize(ActiveAuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme, Roles = "Feature")]
    [Produces("application/json")]
    [Route("api")]
    public class ResourceController : Controller
    {
        private readonly IToggleRepository _toggleRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResourceController(IToggleRepository toggleRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _toggleRepository = toggleRepository;
            _mapper = mapper;
            _userManager = userManager;
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
               
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var toggle = await _toggleRepository.GetToggleByAppName(user.AppKeyName);
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

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var toggle = await _toggleRepository.GetToggleByAppName(user.AppKeyName);
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