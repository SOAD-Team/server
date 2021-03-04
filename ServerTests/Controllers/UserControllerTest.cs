using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Server.Controllers;
using Server.Controllers.Tests;
using Server.Models;
using System.Threading.Tasks;

namespace ServerTests.Controllers
{
    [TestFixture()]
    class UserControllerTest : TestController<UserController>
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            this.controller = new UserController(this.mockMapper, this.mockUserRepository.Object, this.mockUnitOfWork.Object);
        } 

        [Test]
        public async Task RegisterUserFailTest()
        {
            this.mockUserRepository.Setup(_ => _.GetByEmail(It.IsAny<string>())).ReturnsAsync(User.Empty);
            User mockUser = User.Empty;
            var user = await this.controller.RegisterUser(mockUser);
            Assert.IsInstanceOf<ObjectResult>(user);
            var objectResponse = user as ObjectResult;
            Assert.AreEqual(404, objectResponse.StatusCode);
        }

        [Test]
        public async Task RegisterUserSucessTest()
        {
            this.mockUserRepository.Setup(_ => _.GetByEmail(It.IsAny<string>())).ReturnsAsync((User) null);
            User mockUser = User.Empty;
            var user = await this.controller.RegisterUser(mockUser);
            Assert.IsInstanceOf<ObjectResult>(user);
            var objectResponse = user as ObjectResult;
            Assert.AreEqual(200, objectResponse.StatusCode);
        }

        [Test]
        public async Task LoginSucessTest()
        {
            var userTest = new User();
            userTest.Email = "dummy@email.com";
            userTest.Password = "dummyPassword";

            this.mockUserRepository.Setup(_ => _.GetByEmail(It.IsAny<string>())).ReturnsAsync(userTest);

            var user = await this.controller.LogIn(this.mockMapper.Map<Server.Resources.User>(userTest));
            Assert.IsInstanceOf<ObjectResult>(user);
            var objectResponse = user as ObjectResult;
            Assert.AreEqual(200, objectResponse.StatusCode);
        }

        [Test]
        public async Task LoginFailTest()
        {
            this.mockUserRepository.Setup(_ => _.GetByEmail(It.IsAny<string>())).ReturnsAsync((User) null);

            var user = await this.controller.LogIn(Server.Resources.User.Empty);
            Assert.IsInstanceOf<ObjectResult>(user);
            var objectResponse = user as ObjectResult;
            Assert.AreEqual(404, objectResponse.StatusCode);
        }

    }
}
