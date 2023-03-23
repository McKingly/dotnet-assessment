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

        public async Task<List<string>> GetGeneralNews()
        {
            // Get the RSS feed url from the appsettings
            var url = _config["RSS:Url"];

            using var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);

            return feed.Items.Select<SyndicationItem, string>(item => item.Title.Text.ToString())
                .ToList(); //.ToList<string>(); Select(x => x.Title).ToList<string>() .Skip(10) .ToList<string>();

            //throw new System.NotImplementedException();
        }
    }
}
