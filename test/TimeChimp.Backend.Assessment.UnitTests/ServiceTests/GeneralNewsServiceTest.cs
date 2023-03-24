using Elasticsearch.Net;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TimeChimp.Backend.Assessment.Models;
using TimeChimp.Backend.Assessment.Services;
using Xunit;

namespace TimeChimp.Backend.Assessment.UnitTests.ServiceTests
{
    public class GeneralNewsServiceTest
    {
        private readonly IConfiguration config;
        private readonly IMemoryCache cache;

        public GeneralNewsServiceTest()
        {
            //Arrange
            var inMemorySettings = new Dictionary<string, string>()
            {
                { "RSS:Url", "https://www.nu.nl/rss/" }
            };
  
            config= new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            cache = new MemoryCache(new MemoryCacheOptions());

        }

        /// <summary>
        /// Checks to see if the result isn't null
        /// </summary>
        [Fact]
        public void GetNews_OnSuccess_ResultNotNull()
        {
            //Arrange
            var service = new NewsService(config, cache);

            //Act
            var response = service.GetNews(NewsType.Algemeen, null, false, null);

            //Assert
            response.Should().NotBeNull();
        }

        /// <summary>
        /// Test to see id the result is a list of NewsItem
        /// </summary>
        [Fact]
        public void  GetNews_OnSuccess_ResultType()
        {
            //Arrange
            var service = new NewsService(config, cache);

            //Act
            var response = service.GetNews(NewsType.Algemeen, null, false, null);

            //Assert
            response.Should().BeOfType(typeof(List<NewsItem>));
        }

        /// <summary>
        /// Test to see if the cache is working
        /// </summary>
        [Fact]
        public void GetNews_CheckCacheIsWorking()
        {
            //Arrange
            var service = new NewsService(config, cache);

            //Act
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var response = service.GetNews(NewsType.Algemeen, null, false, null);
            stopWatch.Stop();

            TimeSpan ts1 = stopWatch.Elapsed;

            stopWatch.Restart();
            response = service.GetNews(NewsType.Algemeen, null, false, null);
            stopWatch.Stop();

            TimeSpan ts2 = stopWatch.Elapsed;

            //Assert
            Assert.True(ts1 > ts2);
            Assert.True((ts1 - ts2).Milliseconds > 100);
        }


        // Add test to check on invalid URLs

    }
}
