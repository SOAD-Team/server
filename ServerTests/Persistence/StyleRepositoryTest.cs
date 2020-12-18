using NUnit.Framework;
using Server.Models;
using Server.Persistence;
using ServerTests.Persistence.Tests;
using System.Threading.Tasks;

namespace ServerTests.Persistence
{
    class StyleRepositoryTest : RepositoryTests<Style, StyleRepository>
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
            repository = new StyleRepository(context);
        }

        [Test(), Order(1)]
        public async Task CreateTest()
        {
            var value = Style.Empty;
            await base.CreateTest(value);
        }

        [Test(), Order(2)]
        public new async Task GetTest()
        {
            await base.GetTest();
        }

        [Test(), Order(3)]
        public new async Task GetAllTest()
        {
            await base.GetAllTest();
        }
    }
}
