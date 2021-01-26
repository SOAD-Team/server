using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Server;
using Server.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerTests
{
    [TestFixture()]
    class StartupTests
    {
        Startup startup;
        [SetUp]
        public void Setup()
        {

            MockSettings mockSettings = new MockSettings();

            Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
            configurationSectionStub.Setup(x => x["ImagesDatabaseSettings"]).Returns(mockSettings.ToString());
            Mock<Microsoft.Extensions.Configuration.IConfiguration> configurationStub = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            configurationStub.Setup(x => x.GetSection("ImagesDatabaseSettings")).Returns(configurationSectionStub.Object);

            Environment.SetEnvironmentVariable("MONGODB_CONNECTION_STRING", "mongodb://my-movie-image-db:ZIsrJCjPrKnAVv0CdrDICAgcIBu0axTptx8si6pg0DcGoZTRjf4LggBQXt2xdj6ZHPhvQY453famgNUzI0DThg==@my-movie-image-db.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@my-movie-image-db@");

            startup = new Startup(configurationStub.Object);
        }

        [Test()]
        public void ConfigureGenreServicesTest()
        {
            //  Act
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<GenreController>();
            //  Assert
            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<GenreController>();
            Assert.IsNotNull(controller);
        }

        [Test()]
        public void ConfigureLanguageServicesTest()
        {
            //  Act
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<LanguageController>();
            //  Assert
            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<LanguageController>();
            Assert.IsNotNull(controller);
        }

        [Test()]
        public void ConfigureMovieServicesTest()
        {
            //  Act
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<MovieController>();
            //  Assert
            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<MovieController>();
            Assert.IsNotNull(controller);
        }

        [Test()]
        public void ConfigureRecommendationServicesTest()
        {
            //  Act
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<RecommendationController>();
            //  Assert
            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<RecommendationController>();
            Assert.IsNotNull(controller);
        }

        [Test()]
        public void ConfigureReviewServicesTest()
        {
            //  Act
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<ReviewController>();
            //  Assert
            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<ReviewController>();
            Assert.IsNotNull(controller);
        }

        [Test()]
        public void ConfigureStyleServicesTest()
        {
            //  Act
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<StyleController>();
            //  Assert
            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<StyleController>();
            Assert.IsNotNull(controller);
        }

        [Test()]
        public void ConfigureUserServicesTest()
        {
            //  Act
            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<UserController>();
            //  Assert
            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<UserController>();
            Assert.IsNotNull(controller);
        }
    }
}
