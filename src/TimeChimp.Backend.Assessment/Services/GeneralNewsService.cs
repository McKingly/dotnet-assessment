using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using TimeChimp.Backend.Assessment.Interfaces;
using TimeChimp.Backend.Assessment.Models;

namespace TimeChimp.Backend.Assessment.Services
{
    public class GeneralNewsService : IGeneralNewsService
    {
        private readonly IConfiguration _config;

        public GeneralNewsService(IConfiguration config) {
            _config = config;
            //private ICacheProvider _cache;

        }

        /// <summary>
        /// Fetches the list of recent news from the RSS feed for the website Nu.nl
        /// </summary>
        /// <param name="title"></param>
        /// <param name="sortByAsc"></param>
        /// <returns>The list of new titles as a list of strings</returns>
        public async Task<List<NewsItem>> GetGeneralNews(string title, bool sortByAsc)
        {
            // Check to see the cache

            // Get the RSS feed url from the appsettings
            var url = _config["RSS:Url"];

            using var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);

            // Create the response
            var response = feed.Items.Select(item => new NewsItem
            {
                Title = item.Title.Text.ToString(),
                Description = item.Summary.Text.ToString(),
                Creator = item.Authors.Any() ? item.Authors.FirstOrDefault().Name : "Author Unknown",
                PubDate = item.PublishDate.DateTime,
                Categories = item.Categories.Select(cat => cat.Name).ToList()
            }).ToList();

            //var cacheEntryOptions = new MemoryCacheEntryOptions
            //{
            //    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
            //    SlidingExpiration = TimeSpan.FromMinutes(2),
            //    Size = 1024,
            //};
            //_cache.Set(CacheKeys.Employees, response, cacheEntryOptions);

            //Cache end 

            // Filter by name if requested
            if (title != null)
                response = response.Where(item => item.Title.Contains(title)).ToList();

            // Order by title if requested
            if (sortByAsc)
                response = response.OrderBy(item => item.Title).ToList();

            return response;
        }

        /// <summary>
        /// Fetches the list of recent news from the RSS feed that have a specific category associated to it
        /// </summary>
        /// <param name="title"></param>
        /// <param name="sortByAsc"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<List<NewsItem>> GetGeneralNewsByCategory(string title, bool sortByAsc, string category)
        {
            var response = await GetGeneralNews(title, sortByAsc);

            response.RemoveAll(item => !item.Categories.Contains(category));
            return response;
        }
    }
}
