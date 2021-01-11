using NUnit.Framework;
using Server.Models;
using Server.Persistence;
using ServerTests.Persistence.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerTests.Persistence
{
    class MovieDataRepositoryTest : RepositoryTests<MovieData, MovieDataRepository>
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
            repository = new MovieDataRepository(context);
        }

        [Test(), Order(1)]
        public async Task CreateTest()
        {
            var value = MovieData.Empty;
            await base.CreateTest(value);
        }

        [Test(), Order(2)]
        public async Task GetByMovieIdTest()
        {
            var movie = await repository.GetByMovieId(0);
            Assert.IsInstanceOf(typeof(MovieData), movie);
        }

        [Test(), Order(3)]
        public new async Task GetTest()
        {
            var movie = await repository.Get(1);
            Assert.IsInstanceOf(typeof(MovieData), movie);
        }


        [Test(), Order(4)]
        public new async Task GetAllTest()
        {
            await base.GetAllTest();
        }


        [Test(), Order(5)]
        public async Task GetByUserIdTest()
        {
            var movie = await repository.GetByUserId(1);
            Assert.IsInstanceOf(typeof(List<MovieData>), movie);
        }
    }
}
