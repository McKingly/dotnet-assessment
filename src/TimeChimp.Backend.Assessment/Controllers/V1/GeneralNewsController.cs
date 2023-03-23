using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeChimp.Backend.Assessment.Interfaces;
using TimeChimp.Backend.Assessment.Services;

namespace TimeChimp.Backend.Assessment.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class GeneralNewsController : Controller
    {
        public readonly IGeneralNewsService _generalNewsService;

        public GeneralNewsController( IGeneralNewsService generalNewsService) {
            _generalNewsService = generalNewsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNews() {
            var response = await _generalNewsService.GetGeneralNews();
            return new OkObjectResult(response); 
        }

        //public IActionResult Index()
        //{

        //    return View();
        //}
    }
}
