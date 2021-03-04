using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Server.Controllers;
using Server.Controllers.Tests;
using System.Threading.Tasks;

namespace ServerTests.Controllers
{
    class RecomendationControllerTest : TestController<RecommendationController>
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            this.controller = new RecommendationController(this.mockMapper, this.mockReviewRepository.Object, this.mockMovieDataRepository.Object, this.mockMovieRepository.Object, this.mockMovieDataGenreRepository.Object);
        }

        [Test]
        public async Task PutTest()
        {
            var recommendations = await this.controller.Put(Server.Resources.UserPoints.Empty);
            Assert.IsInstanceOf<ObjectResult>(recommendations);
            var objectResult = recommendations as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
        }
    }
}
