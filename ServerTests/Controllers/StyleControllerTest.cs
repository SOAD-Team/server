
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Server.Controllers;
using Server.Controllers.Tests;
using System.Threading.Tasks;

namespace ServerTests.Controllers
{
    class StyleControllerTest : TestController<StyleController>
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            this.controller = new StyleController(this.mockStyleRepository.Object, this.mockMapper);
        }

        [Test]
        public async Task PostTest()
        {
            var styles = await this.controller.GetStyles();
            Assert.IsInstanceOf<ObjectResult>(styles);
            var objectResult = styles as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
        }
    }
}
