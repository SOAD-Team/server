using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Server.Controllers;
using Server.Controllers.Tests;
using System.Threading.Tasks;

namespace ServerTests.Controllers
{
    class ReviewControllerTest : TestController<ReviewController>
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            this.controller = new ReviewController(this.mockReviewRepository.Object, this.mockMapper, this.mockUnitOfWork.Object);
        }

        [Test]
        public async Task GetReviewTest()
        {
            var reviews = await this.controller.GetReview(1);
            Assert.IsInstanceOf<ObjectResult>(reviews);
            var objectResult = reviews as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [Test]
        public async Task CreateReviewTest()
        {
            var reviews = await this.controller.CreateReview(Server.Resources.Review.Empty);
            Assert.IsInstanceOf<ObjectResult>(reviews);
            var objectResult = reviews as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
        }
    }
}
