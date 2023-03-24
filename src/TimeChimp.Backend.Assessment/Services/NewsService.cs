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
    public class NewsService : INewsService
    {
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;

        public NewsService(IConfiguration config, IMemoryCache cache) {
            _config = config;
            _cache = cache;
        }

        /// <summary>
        /// Fetches the list of recent news from the RSS feed for the website Nu.nl
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title"></param>
        /// <param name="sortByAsc"></param>
        /// <param name="category"></param>
        /// <returns>The list of new titles as a list of strings</returns>
        public List<NewsItem> GetNews(NewsType type, string title, bool sortByAsc, string category)
        {
            try
            {
                IEnumerable<NewsItem> response;

                string cacheKey = type.ToString();

                // Check to see if the cache has data.
                // If not, request information from the feed
                // If yes, return the information present in the cache 
                if (!_cache.TryGetValue(cacheKey, out response))
                {
                    // Get the RSS feed url from the appsettings
                    var url = _config["RSS:Url"] + cacheKey;

                    // Read from the RSS feed
                    using var reader = XmlReader.Create(url);
                    var feed = SyndicationFeed.Load(reader);

                    // Create the response
                    response = feed.Items.Select(item => new NewsItem
                    {
                        Title = item.Title.Text.ToString(),
                        Description = item.Summary.Text.ToString(),
                        Creator = item.Authors.Any() ? item.Authors.First().Name : "Author Unknown",
                        PubDate = item.PublishDate.DateTime,
                        Categories = item.Categories.Select(cat => cat.Name).ToList()
                    }).ToList();

                    // Create the cache options object for use when setting the cache
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(10))   // Extends the time the cache can be inactive
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(60)); // Sets the absolute expiration date for the cache entry

                    // Add the data to the cache
                    _cache.Set(cacheKey, response, cacheEntryOptions);
                }

                // Filter by name if requested
                if (title != null)
                    response = response.Where(item => item.Title.Contains(title)).ToList();

                // Order by title if requested
                if (sortByAsc)
                    response = response.OrderBy(item => item.Title).ToList();

                // Remove all news that don't belong to a specific category
                if (category != null)
                    response = response.Where(item => item.Categories.Contains(category));

                return response.ToList();
            }
            // For when the RSS Feed Url is invalid
            catch (UriFormatException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
