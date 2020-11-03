using Server.Models;
using NUnit.Framework;

namespace Server.Models.Tests
{
    [TestFixture()]
    public class LanguageTest
    {
        [Test()]
        public void ConstructorLanguageTest()
        {
            var data = new Language("");
            Assert.IsInstanceOf(typeof(Language), data);
        }

        [Test()]
        public void ConstructorLanguageTest1()
        {
            var data = new Language(0,"");
            Assert.IsInstanceOf(typeof(Language), data);
        }
    }
}
