using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Server.Persistence;
using System.Threading.Tasks;

namespace ServerTests.Persistence.Tests
{
    public class RepositoryTests<Model, Repository> where Repository : Repository<Model>
    {
        protected Repository repository;
        protected MoviesDB context;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MoviesDB>()
                .UseInMemoryDatabase(databaseName: "Movies Test").Options;
            context = new MoviesDB(options);
        }

        public async Task CreateTest(Model value)
        {
            value = await repository.Create(value);
            await context.SaveChangesAsync();
            Assert.IsInstanceOf(typeof(Model), value);
        }


        public async Task GetTest()
        {
            var value = await repository.Get(1);
            Assert.IsInstanceOf(typeof(Model), value);
        }


        public async Task GetAllTest()
        {
            var value = await repository.GetAll();
            Assert.IsNotEmpty(value);
        }
    }
}