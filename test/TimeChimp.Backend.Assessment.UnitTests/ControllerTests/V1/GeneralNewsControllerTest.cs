using FluentAssertions;
using System.Collections.Generic;
using Moq;
using TimeChimp.Backend.Assessment.Controllers.V1;
using TimeChimp.Backend.Assessment.Interfaces;
using TimeChimp.Backend.Assessment.Models;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TimeChimp.Backend.Assessment.UnitTests.ControllerTests.V1
{
    public class GeneralNewsControllerTest
    {
        private readonly Mock<INewsService> newsService;

        public GeneralNewsControllerTest()
        {
            newsService = new Mock<INewsService>();
        }

        [Fact]
        public void GetGeneralNews_OnSuccess_ResultNotNullAsync()
        {
            //Arrange 
            var controller = CreateDefaultNewsController(); 

            //Act
            var response = controller.GetNews(NewsType.Algemeen, false, null, null);

            //Assert
            response.Should().NotBeNull();
        }

        [Theory]
        [InlineData(NewsType.Algemeen, null, false, null)]
        [InlineData(NewsType.Economie, null, false, null)]
        [InlineData(NewsType.Muziek, null, false, null)]
        [InlineData(NewsType.Sport, null, false, null)]
        [InlineData(NewsType.Algemeen, null, true, null)]
        public void GetGeneralNews_OnSuccess_Return200Code(NewsType type, string title, bool sortByAsc, string category)
        {
            //Arrange 
            var controller = CreateDefaultNewsController();

            //Act
            var response = (OkObjectResult) controller.GetNews(type, sortByAsc, title, category);

            //Assert
            response.StatusCode.Should().Be(200);
        }

        private NewsController CreateDefaultNewsController()
        {
            return new NewsController(newsService.Object);
        } 
    }
}
