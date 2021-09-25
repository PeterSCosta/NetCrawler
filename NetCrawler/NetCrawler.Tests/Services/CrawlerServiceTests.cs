using Moq;
using NetCrawler.Models;
using NUnit.Framework;
using System;

namespace NetCrawler.Services.Tests
{
    [TestFixture]
    public class CrawlerServiceTests
    {
        [TestCase]
        public void ExecuteTest()
        {
            //Arrange
            var model = new CrawlerViewModel();
            var service = Mock.Of<ICrawlerService>();

            //Assert
            var ex = Assert.Throws<Exception>(() => service.Execute(model));
            Assert.That(ex.Message, Is.EqualTo("Website's url is required"));
        }
    }
}