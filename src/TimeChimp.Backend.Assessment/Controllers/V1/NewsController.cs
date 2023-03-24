using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
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
    public class NewsController : Controller
    {
        public readonly INewsService _newsService;

        public NewsController(INewsService newsService) {
            _newsService = newsService;
        }

        /// <summary>
        /// GET method responsible for returning a list of a certain type of news
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title">Filter news list by title</param>
        /// <param name="ascending"  >Sort list os news by their titles alphabetical order</param>
        /// <param name="category">Filter news list by category</param>
        /// <returns>List of news of a certain type</returns>>
        [HttpGet]
        [Route("{type}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NewsItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNews(
            NewsType type,
            bool ascending,
            string title,
            string category
            )
        {
            var response = _newsService.GetNews(type, title, ascending, category);
            return new OkObjectResult(response);
        }
    }
}
