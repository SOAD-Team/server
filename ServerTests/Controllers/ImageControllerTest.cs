using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Server.Controllers;
using Server.Controllers.Tests;
using Server.Models;
using System.IO;
using System.Threading.Tasks;

namespace ServerTests.Controllers
{
    class ImageControllerTest : TestController<ImageController>
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            this.controller = new ImageController(this.mockImageRepository.Object, this.mockMapper);
        }

        [Test]
        public async Task CreateImageTest()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var image = await this.controller.CreateImage(fileMock.Object);
            Assert.IsInstanceOf<ObjectResult>(image);
            var objectResponse = image as ObjectResult;
            Assert.AreEqual(200, objectResponse.StatusCode);
        }

        [Test]
        public async Task GetMovieImagesTest()
        {
            var image = await this.controller.GetMovieImages("mockID");
            Assert.IsInstanceOf<FileContentResult>(image);
        }
    }
}
