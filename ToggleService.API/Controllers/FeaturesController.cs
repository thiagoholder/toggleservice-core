using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ToggleService.API.Helpers;
using ToggleService.API.Models;
using ToggleService.DataMongoDB.Repository;

namespace ToggleService.API.Controllers
{
    [RoutePrefix("api/toggles")]
    public class FeaturesController : ApiController
    {
        private readonly IToggleRepository _toggleRepository;
        private readonly ToogleFactory _toggleFactory = new ToogleFactory();

        public FeaturesController(IToggleRepository toggleRepository)
        {
            _toggleRepository = toggleRepository;
        }

        [Route("{nameFeature}")]
        [ResponseType(typeof(ToggleModel))]
        [HttpGet]
        public async Task<IHttpActionResult> GetToggleFeature(string nameFeature)
        {
            try
            {
                IEnumerable<string> headerValues;
                var uniqueServiceKey = string.Empty;

                if (Request.Headers.TryGetValues("uniqueServiceKey", out headerValues))
                {
                    uniqueServiceKey = headerValues.FirstOrDefault();
                }

                Request.GetHeader("uniqueServiceKey");

                var toggle = await _toggleRepository.GetToggle(uniqueServiceKey);
                if (toggle == null)
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
    }
}
