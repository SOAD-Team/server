using NUnit.Framework;
using Server.Models;
using Server.Persistence;
using ServerTests.Persistence.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerTests.Persistence
{
    class ReviewRepositoryTest : RepositoryTests<Review, ReviewRepository>
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
            repository = new ReviewRepository(context);
        }

        [Test(), Order(1)]
        public async Task CreateTest()
        {
            var value = Review.Empty;
            await base.CreateTest(value);
        }

        [Test(), Order(2)]
        public async Task GetByMovieIdTest()
        {
            var value = await repository.GetbyMovieId(0);
            Assert.IsInstanceOf(typeof(List<Review>), value);
        }
    }
}
