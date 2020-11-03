using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Server.Models;
using System;
using System.IO;

namespace Server.DTOs.Tests
{
    [TestFixture()]
    public class ImageTests
    {
        [Test()]
        public void MapToImageTest()
        {
            Image data = Image.Empty;

            //Arrange
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

            var file = fileMock.Object;

            data.ObjectImage = new FileModel("",fileMock.Object);
            Assert.IsInstanceOf(typeof(Models.Image), data.MapToImage());
        }
    }
}