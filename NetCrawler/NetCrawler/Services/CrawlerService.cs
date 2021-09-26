using HtmlAgilityPack;
using NetCrawler.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetCrawler.Services
{
    public class CrawlerService : ICrawlerService
    {
        private readonly int NUMBER_COMMOM_WORDS_DEFAULT = 10;
        private readonly IHttpService _httpService;

        public CrawlerService(
            IHttpService httpService    
        )
        {
            _httpService = httpService;
        }

        public async Task<CrawlerViewModel> Execute(
            CrawlerViewModel model
        )
        {
            Validate(model);

            var htmlDocument = await _httpService.Execute(model.Url);

            model.Images = GetImages(htmlDocument);
            model.CommomWords = GetMostCommomWords(htmlDocument);

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

        private List<ImageModel> GetImages(
            HtmlDocument htmlDocument    
        )
        {
            var images =
                htmlDocument
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

        private List<CommomWordModel> GetMostCommomWords(
            HtmlDocument htmlDocument
        )
        {
            TextInfo info = CultureInfo.CurrentCulture.TextInfo;

            var commomWords =
                htmlDocument
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