using HtmlAgilityPack;
using NetCrawler.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetCrawler.Services
{
    public class CrawlerService : ICrawlerService
    {
        private readonly int NUMBER_COMMOM_WORDS_DEFAULT = 10;
        private string _html;
        private HttpClient _httpClient;
        private HtmlDocument _htmlDocument;

        public CrawlerService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<CrawlerViewModel> Execute(
            CrawlerViewModel model
        )
        {
            Validate(model);

            _html = await _httpClient.GetStringAsync(model.Url);
            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(_html);

            model.Images = GetImages();
            model.CommomWords = GetMostCommomWords();

            return model;
        }

        private void Validate(
            CrawlerViewModel model
        )
        {
            if (string.IsNullOrWhiteSpace(model.Url))
            {
                throw new Exception("Website's url is required");
            }
        }

        private List<ImageModel> GetImages()
        {
            var images =
                _htmlDocument
                .DocumentNode
                .Descendants("img")
                .Where(i => i.Attributes.Contains("src"))
                .Select(i => new ImageModel()
                {
                    Url = i.Attributes["src"].Value
                })
                .ToList();

            return images;
        }

        private List<CommomWordModel> GetMostCommomWords()
        {
            TextInfo info = CultureInfo.CurrentCulture.TextInfo;

            var commomWords =
                _htmlDocument
                .DocumentNode
                .DescendantsAndSelf()
                .Where(n => !n.HasChildNodes)
                .Where(n => !string.IsNullOrEmpty(Regex.Replace(n.InnerText, @"[^0-9a-zA-Z:,]+", "")))
                .GroupBy(w => info.ToTitleCase(w.InnerText))
                .Select(item => new CommomWordModel()
                {
                    Quantity = item.Count(),
                    Value = item.Key
                })
                .OrderByDescending(x => x.Quantity)
                .Take(NUMBER_COMMOM_WORDS_DEFAULT)
                .Select((item, index) =>
                {
                    item.Position = index + 1;
                    return item;
                })
                .ToList();

            return commomWords;
        }
    }
}