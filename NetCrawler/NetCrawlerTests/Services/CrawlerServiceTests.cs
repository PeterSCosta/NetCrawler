using HtmlAgilityPack;
using Moq;
using NetCrawler.Models;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            var httpServiceMock = Mock.Of<IHttpService>();
            var service = new CrawlerService(httpServiceMock);

            //Assert
            var ex = Assert.ThrowsAsync<Exception>(() => service.Execute(model));
            Assert.That(ex.Message, Is.EqualTo("Website's url is required"));
        }

        [Test]
        public async Task ExecuteTestWithUrl_ShouldFindImagesAndWords()
        {
            //Arrange
            var expectedImages = 2;
            var expectedWords = 2;
            var htmlDocument = new HtmlDocument();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Services", @"Teste.html");
            htmlDocument.Load(path);
            var model = new CrawlerViewModel();
            model.Url = path;
            var httpServiceMock = new Mock<IHttpService>();
            httpServiceMock
                .Setup(s => s.Execute(path))
                .ReturnsAsync(htmlDocument);

            var service = new CrawlerService(httpServiceMock.Object);

            //Act
            await service.Execute(model);

            //Assert
            Assert.AreEqual(expectedImages, model.Images.Count());
            Assert.AreEqual(expectedWords, model.CommomWords.Count());
        }
    }
}