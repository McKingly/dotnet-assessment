using FluentAssertions;
using System.Collections.Generic;
using Moq;
using TimeChimp.Backend.Assessment.Controllers.V1;
using TimeChimp.Backend.Assessment.Interfaces;
using TimeChimp.Backend.Assessment.Models;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;
using Xunit.Sdk;

namespace TimeChimp.Backend.Assessment.UnitTests.ControllerTests.V1
{
    public class GeneralNewsControllerTest
    {
        private readonly Mock<IGeneralNewsService> generalNewsService;

        public GeneralNewsControllerTest()
        {
            generalNewsService = new Mock<IGeneralNewsService>();
        }

        [Fact]
        public async Task GetGeneralNews_OnSuccess_ResultNotNullAsync()
        {
            //Arrange 
            var controller = CreateDefaultGeneralNewsController(); 

            //Act
            var response = await controller.GetNews(null, false);

            //Assert
            response.Should().NotBeNull();
        }

        //[Fact]
        //public async Task GetGeneralNews_OnSuccess_Return200Code()
        //{
        //    //Arrange 
        //    var controller = CreateDefaultGeneralNewsController();

        //    //Act
        //    var response = (ObjectResult)await controller.GetNews(null, false);

        //    //Assert
        //    response.StatusCode.Should().Be(200);
        //}

        [Theory]
        [InlineData(null, false)]
        [InlineData(null, true)]
        [InlineData("", false)]
        [InlineData("", true)]
        public async Task GetGeneralNews_OnSuccess_Return200Code(string title, bool sortByAsc)
        {
            //Arrange 
            var controller = CreateDefaultGeneralNewsController();

            //Act
            var response = (ObjectResult)await controller.GetNews(title, sortByAsc);

            //Assert
            response.StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task GetGeneralNews_OnSuccess_ReturnListOfNewsItems()
        {
            //Arrange 
            var controller = CreateDefaultGeneralNewsController();

            //Act
            var response = (OkObjectResult)await controller.GetNews(null, false);
             
            //Assert
            Assert.IsType<List<NewsItem>>(response.Value);
        }

        [Fact]
        [InlineData]
        public async Task GetGeneralNewsByCategory_OnSuccess_ResultNotNullAsync()
        {
            //Arrange 
            var controller = CreateDefaultGeneralNewsController();

            //Act
            var response = await controller.GetNewsByCategory(null, false, null);

            //Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task GetGeneralNewsByCategory_OnSuccess_Return200Code()
        {
            //Arrange 
            var controller = CreateDefaultGeneralNewsController();

            //Act
            var response = (ObjectResult)await controller.GetNewsByCategory(null, false, null);

            //Assert
            response.StatusCode.Should().Be(200);

        }

        private GeneralNewsController CreateDefaultGeneralNewsController()
        {
            return new GeneralNewsController(generalNewsService.Object);
        } 
    }
}
