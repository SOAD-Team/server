using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Server.Controllers;
using Server.Models;

namespace ServerTests.Controllers
{
    [TestFixture]
    class RegisterUserControllerTest
    {
        private RegisterUserController controller;
        private MoviesDB context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MoviesDB>().UseInMemoryDatabase(databaseName: "Movies Test").Options;
            context = new MoviesDB(options);
            controller = new RegisterUserController(context);
        }

        [Test()]
        public void RegisterUserTestAsync()
        {
            var data = Server.DTOs.User.Empty;
            data.Email = "pruebaemail12@gmail.com";
            var result = controller.RegisterUser(data);
            Assert.AreEqual(result, 1);
        }


        [Test()]
        public void FailRegisterUserTestAsync()
        {
            var data = Server.DTOs.User.Empty;
            data.Email = "pruebaemail@gmail.com";
            controller.RegisterUser(data);

            var result = controller.RegisterUser(data);
            Assert.AreEqual(result, 0);
        }

        [Test()]
        public void LogInTest()
        {
            var data = Server.DTOs.User.Empty;
            data.Email = "pruebaemail1@gmail.com";
            data.Password = "123456";
            if (controller.RegisterUser(data) == 1)
            {
                var user = new Server.DTOs.UserData();
                user.Email = data.Email;
                user.Password = data.Password;
                var result = controller.LogIn(user);
                Assert.IsInstanceOf(typeof(Server.DTOs.User), result);
            }
            else Assert.Fail();
        }

        [Test()]
        public void LogInTestFail()
        {
            var user = new Server.DTOs.UserData();
            user.Email = "pruebas7566@gamil.com";
            user.Password = "45668546354";
            var result = controller.LogIn(user);
            Assert.IsNull(result);
        }
    }
}
