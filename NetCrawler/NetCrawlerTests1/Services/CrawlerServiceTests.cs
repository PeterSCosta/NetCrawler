using Moq;
using NetCrawler.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace NetCrawler.Services.Tests
{
    [TestFixture]
    public class CrawlerServiceTests
    {
        [Test]
        public void ExecuteTestWithoutUrl_ShouldThrowException()
        {
            //Arrange
            var model = new CrawlerViewModel();
            var service = new CrawlerService();

            //Assert
            var ex = Assert.ThrowsAsync<Exception>(() => service.Execute(model));
            Assert.That(ex.Message, Is.EqualTo("Website's url is required"));
        }
    }
}