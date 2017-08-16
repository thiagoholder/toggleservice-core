using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToggleService.Data.Repository.Interface;

namespace ToggleService.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/administrator")]
    public class AdministratorController : Controller
    {
        public readonly IToggleRepository _repository;
        public AdministratorController(IToggleRepository repository)
        {
            _repository = repository;
        }

        [Route("toggles")]
        [HttpGet]
        public async Task<IActionResult> GetAllToggles()
        {
            var teste = await _repository.GetAllToggles();
            return Content($"{teste}");
        }
    }
}