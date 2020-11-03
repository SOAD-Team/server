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

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddTransient<Logger>();
            services.AddTransient<ImagesDB>();
            services.AddTransient<MoviesDB>();
            services.AddTransient<RegisterUserController>();

            var serviceProvider = services.BuildServiceProvider();

            controller = serviceProvider.GetService<RegisterUserController>();
        }

        [Test()]
        public void RegisterUserTestAsync()
        {
            Mock<Server.DTOs.User> mock = new Mock<Server.DTOs.User>(Server.DTOs.MovieData.Empty);
            var result = controller.RegisterUser(mock.Object);
            Assert.AreEqual(result, 1);
        }
    }
}
