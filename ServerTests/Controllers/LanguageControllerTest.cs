using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Server.Controllers;
using Server.Controllers.Tests;
using System.Threading.Tasks;

namespace ServerTests.Controllers
{
    class LanguageControllerTest : TestController<LanguageController>
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            this.controller = new LanguageController(this.mockLanguageRepository.Object, this.mockMapper);
        }

        [Test]
        public async Task GetLanguagesTest()
        {
            var languages = await this.controller.GetLanguages();
            Assert.IsInstanceOf<ObjectResult>(languages);
            var objectResponse = languages as ObjectResult;
            Assert.AreEqual(200, objectResponse.StatusCode);
        }
    }
}
