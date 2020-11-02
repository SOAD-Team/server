using NUnit.Framework;
using Server.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Server.Controllers.Tests
{
    [TestFixture()]
    public class MovieControllerTest
    {

        private MovieController controller;
        private MoviesDB context;
    
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MoviesDB>()
            .UseInMemoryDatabase(databaseName: "Movies Test").Options;
            context = new MoviesDB(options);
            Mock<IImagesDB> mongoContextStub = new Mock<IImagesDB>(MockBehavior.Loose);
            mongoContextStub.Setup(_ => _.Create(It.IsAny<Image>())).Returns<Image>(im => im);
            mongoContextStub.Setup(_ => _.Get(It.IsAny<string>())).Returns(Image.Empty);
            controller = new MovieController(context, mongoContextStub.Object);
            
        }
        [Test()]
        public void GetMoviesTest()
        {
            context.Movie.Add(Movie.Empty);
            context.SaveChanges();
            Assert.IsNotEmpty(controller.GetMovies());
        }

        [Test()]
        public void GetGenresTest()
        {
            context.Genre.Add(Genre.Empty);
            context.SaveChanges();
            Assert.IsNotEmpty(controller.GetGenres());
        }

        [Test()]
        public void GetLanguagesTest()
        {
            context.Language.Add(Language.Empty);
            context.SaveChanges();
            Assert.IsNotEmpty(controller.GetLanguages());
        }

        [Test()]
        public void GetStylesTest()
        {
            context.Style.Add(Style.Empty);
            context.SaveChanges();
            Assert.IsNotEmpty(controller.GetStyles());
        }
        [Test()]
        public void CreateImageTest()
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

            var result = controller.CreateImage(file);

            //Assert
            Assert.IsInstanceOf(typeof(Task<DTOs.Image>), result);
        }

        [Test()]
        public void CreateMovieTest()
        {
            var result = controller.CreateMovie(DTOs.MovieData.Empty);
            Assert.IsInstanceOf(typeof(DTOs.MovieData), result);
        }
    }
}
