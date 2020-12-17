using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Server.Controllers;
using Server.Controllers.Tests;
using System.Threading.Tasks;

namespace ServerTests.Controllers
{
    class MovieControllerTest : TestController<MovieController>
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            this.controller = new MovieController(this.mockMapper, 
                this.mockMovieRepository.Object, 
                this.mockMovieDataRepository.Object, 
                this.mockMovieDataGenreRepository.Object, 
                this.mockMovieDataLanguageRepository.Object, 
                this.mockUnitOfWork.Object);
        }

        [Test]
        public async Task PostTest()
        {
            var movieData = await this.controller.Post(Server.Resources.Movie.Empty);
            Assert.IsInstanceOf<ObjectResult>(movieData);
            var objectResult = movieData as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [Test]
        public async Task GetAllTest()
        {
            var movieData = await this.controller.GetAll();
            Assert.IsInstanceOf<ObjectResult>(movieData);
            var objectResult = movieData as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [Test]
        public async Task GetByUserIdTest()
        {
            var movieData = await this.controller.GetMovieByUserId(1);
            Assert.IsInstanceOf<ObjectResult>(movieData);
            var objectResult = movieData as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [Test]
        public async Task GetTest()
        {
            var movieData = await this.controller.Get(1);
            Assert.IsInstanceOf<ObjectResult>(movieData);
            var objectResult = movieData as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
        }


        [Test]
        public async Task Put()
        {
            var movieData = await this.controller.Put(Server.Resources.Movie.Empty);
            Assert.IsInstanceOf<ObjectResult>(movieData);
            var objectResult = movieData as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
        }
    }
}
