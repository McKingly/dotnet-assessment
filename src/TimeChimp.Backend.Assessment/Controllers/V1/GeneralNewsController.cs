using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TimeChimp.Backend.Assessment.Interfaces;
using TimeChimp.Backend.Assessment.Models;
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

        public GeneralNewsController(IGeneralNewsService generalNewsService) {
            _generalNewsService = generalNewsService;
        }   

        /// <summary>
        /// GET method responsible for returning the list of recent general news
        /// </summary>
        /// <param name="title">Filter by title</param>
        /// <param name="ascending"></param>
        /// <returns>A list of NewsItem objects</returns>
        [HttpGet]
        //[Route("{type}")]
        public async Task<IActionResult> GetNews(
            //NewsType type,
            string title,
            bool ascending
            //string category
            )
        {
            var response = await _generalNewsService.GetGeneralNews(title, ascending);
            return new OkObjectResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ascending"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{category}")]
        public async Task<IActionResult> GetNewsByCategory(string title,
            bool ascending,
            string category
            )
        {
            var response = await _generalNewsService.GetGeneralNewsByCategory(title, ascending, category);
            return new OkObjectResult(response);
        }

    }
}
