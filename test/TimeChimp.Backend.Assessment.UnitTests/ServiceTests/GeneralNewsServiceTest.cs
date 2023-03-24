using Castle.Core.Configuration;
using Moq;
using TimeChimp.Backend.Assessment.Services;
using Xunit;

namespace TimeChimp.Backend.Assessment.UnitTests.ServiceTests
{
    public class GeneralNewsServiceTest
    {
        private readonly Mock<IConfiguration> config;
        public GeneralNewsServiceTest()
        {
            config = new Mock<IConfiguration>();
        }

        //[Fact]
        //public void GivenNothing()
        //{   
        //    //var service = new GeneralNewsService(config.Object);

        //}

    }
}
