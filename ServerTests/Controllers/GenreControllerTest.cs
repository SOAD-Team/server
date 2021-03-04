using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Server.Controllers;
using Server.Controllers.Tests;
using System.Threading.Tasks;

namespace ServerTests.Controllers
{
    class GenreControllerTest : TestController<GenreController>
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            this.controller = new GenreController(this.mockGenreRepository.Object, this.mockMapper);
        }

        [Test]
        public async Task GetGenresTest()
        {
            var genres = await this.controller.GetGenres();
            Assert.IsInstanceOf<ObjectResult>(genres);
            var objectResponse = genres as ObjectResult;
            Assert.AreEqual(200, objectResponse.StatusCode);
        }
    }
}
