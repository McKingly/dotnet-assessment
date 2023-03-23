using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using TimeChimp.Backend.Assessment.Interfaces;

namespace TimeChimp.Backend.Assessment.Services
{
    public class GeneralNewsService : IGeneralNewsService
    {
        private readonly IConfiguration _config;

        public GeneralNewsService(IConfiguration config) {
            _config = config;
        }

        /// <summary>
        /// Fetches the list of recent news from the RSS feed for the website Nu.nl
        /// </summary>
        /// <returns>The list of new titles as a list of strings</returns>
        public async Task<List<string>> GetGeneralNews(string titleFilter, bool sortByTitle)
        {
            // Get the RSS feed url from the appsettings
            var url = _config["RSS:Url"];

            using var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);

            // Create the response
            var response = feed.Items.Select<SyndicationItem, string>(item => item.Title.Text.ToString())
                .ToList();

            // Filter by name if requested
            if (titleFilter != null)
                response = response.Where(item => item.Contains(titleFilter)).ToList();

            // Order by title if requested
            if (sortByTitle)
                response = response.OrderBy(item => item).ToList();

            return response;
        }
    }
}
