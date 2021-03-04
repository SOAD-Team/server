using NUnit.Framework;
using Server.Models;
using Server.Persistence;
using ServerTests.Persistence.Tests;
using System.Threading.Tasks;

namespace ServerTests.Persistence
{
    class UserRepositoryTest : RepositoryTests<User, UserRepository>
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
            repository = new UserRepository(context);
        }

        [Test(), Order(1)]
        public async Task CreateTest()
        {
            var value = User.Empty;
            value.Email = "empty@email.com";
            await base.CreateTest(value);
        }

        [Test(), Order(2)]
        public new async Task GetTest()
        {
            var value = await repository.Get(1);
            Assert.IsInstanceOf(typeof(User), value);
        }

        [Test(), Order(3)]
        public  async Task GetByEmailTest()
        {
            var value = await repository.GetByEmail("empty@email.com");
            Assert.IsInstanceOf(typeof(User), value);
        }
    }
}
