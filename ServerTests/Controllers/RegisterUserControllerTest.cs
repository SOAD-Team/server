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
            data.Email = "pruebaemail1@gmail.com";
            var result = controller.RegisterUser(data);
            Assert.AreEqual(result, 1);
        }


        [Test()]
        public void FailRegisterUserTestAsync()
        {
            var data = Server.DTOs.User.Empty;
            data.Email = "pruebaemail@gmail.com";
            var result = controller.RegisterUser(data);
            result = controller.RegisterUser(data);
            Assert.AreEqual(result, 0);
        }

        [Test()]
        public void LogInTest()
        {
            var user = new Server.DTOs.UserData();
            user.Email = "pruebaemail@gmail.com";
            user.Password = "";
            var result = controller.LogIn(user);
            Assert.IsInstanceOf(typeof(Server.DTOs.User), result);

        }
    }
}
