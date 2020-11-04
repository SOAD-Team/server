using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Server.Controllers;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            var result = controller.RegisterUser(data);
            Assert.AreEqual(result, 1);
        }
    }
}
