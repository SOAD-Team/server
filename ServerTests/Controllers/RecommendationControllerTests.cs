using NUnit.Framework;
using Server.Models;
using Moq;
using NUnit.Framework.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Server.Controllers.Tests
{
    [TestFixture()]
    public class RecommendationControllerTests
    {
        private UserPointsController controller;
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
            var imgList = new List<Image>();
            imgList.Add(Image.Empty);
            mongoContextStub.Setup(_ => _.Get()).Returns(imgList);
            controller = new UserPointsController(context, mongoContextStub.Object);

        }
        [Test(), Order(25)]
        public void PostTest()
        {
            var userPoints = Resources.UserPoints.Empty;
            var movies = controller.Post(userPoints);

            Assert.IsInstanceOf(typeof(IEnumerable<Resources.Recommendation>),movies);
        }
    }
}