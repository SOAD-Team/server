using NUnit.Framework;
using Server.Models;
using Server.Persistence;
using ServerTests.Persistence.Tests;
using System.Threading.Tasks;

namespace ServerTests.Persistence
{
    class MovieRepositoryTest : RepositoryTests<Movie, MovieRepository>
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
            repository = new MovieRepository(context);
        }

        [Test(), Order(1)]
        public async Task CreateTest()
        {
            var value = Movie.Empty;
            await base.CreateTest(value);
        }

        [Test(), Order(3)]
        public new async Task GetTest()
        {
            await base.GetTest();
        }

        [Test(), Order(4)]
        public new async Task GetAllTest()
        {
            await base.GetAllTest();
        }
    }
}
