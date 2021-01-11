using NUnit.Framework;
using Server.Models;
using Server.Persistence;
using ServerTests.Persistence.Tests;
using System.Threading.Tasks;

namespace ServerTests.Persistence
{
    class MovieDataLanguageRepositoryTest : RepositoryTests<MovieDataLanguage, MovieDataLanguageRepository>
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
            repository = new MovieDataLanguageRepository(context);
        }

        [Test(), Order(1)]
        public async Task CreateTest()
        {
            var value = MovieDataLanguage.Empty;
            await base.CreateTest(value);
        }

        [Test(), Order(2)]
        public new async Task GetAllTest()
        {
            await base.GetAllTest();
        }
    }
}
