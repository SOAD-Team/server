using NUnit.Framework;
using Server.Controllers;
using Microsoft.Extensions.Logging;
using Server.Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace Server.Controllers.Tests
{
    [TestFixture()]
    public class MovieControllerTest
    {

        private MovieController controller;
    
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddTransient<Logger>();
            services.AddTransient<ImagesDB>();
            services.AddTransient<MoviesDB>();
            services.AddTransient<MovieController>();

            var serviceProvider = services.BuildServiceProvider();

            controller = serviceProvider.GetService<MovieController>();
        }
        [Test()]
        public void GetTest()
        {
            Assert.IsNotEmpty(controller.GetMovies());
        }

        [Test()]
        public void GetGenresTest()
        {
            Assert.IsNotEmpty(controller.GetGenres());
        }

        [Test()]
        public void GetLanguagesTest()
        {
            Assert.IsNotEmpty(controller.GetLanguages());
        }

        [Test()]
        public void GetStylesTest()
        {
            Assert.IsNotEmpty(controller.GetStyles());
        }
        [Test()]
        public async Task CreateImageTestAsync()
        {
            //Arrange
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            var result = await controller.CreateImage(file);

            //Assert
            Assert.IsInstanceOf(typeof(Task<DTOs.Image>), result);
        }

        [Test()]
        public void CreateMovieTestAsync()
        {
            Mock<DTOs.MovieData> mock = new Mock<DTOs.MovieData>(DTOs.MovieData.Empty);
            var result = controller.CreateMovie(mock.Object);
            Assert.IsInstanceOf(typeof(Task<DTOs.MovieData>), result);
        }
    }
}
