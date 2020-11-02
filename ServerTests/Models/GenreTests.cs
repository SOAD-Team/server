using NUnit.Framework;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models.Tests
{
    [TestFixture()]
    public class GenreTests
    {
        [Test()]
        public void ConstructorGenreTest()
        {
            var data = new Genre("");
            Assert.IsInstanceOf(typeof(Genre), data);
        }

        [Test()]
        public void ConstructorGenreTest1()
        {
            var data = new Genre(0, "");
            Assert.IsInstanceOf(typeof(Genre), data);
        }
    }
}