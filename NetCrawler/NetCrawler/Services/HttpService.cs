using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCrawler.Services
{
    public class HttpService : IHttpService
    {
        public async Task<HtmlDocument> Execute(
            string url
        )
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            return htmlDocument;
        }
    }
}