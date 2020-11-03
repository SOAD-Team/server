using NUnit.Framework;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models.Tests
{
    [TestFixture()]
    public class ImageTests
    {
        [Test()]
        public void ConstructorImageTest()
        {
            var data = new Image("",new byte[0]);
            Assert.IsInstanceOf(typeof(Image), data);
        }
    }
}