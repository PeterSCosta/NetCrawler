using HtmlAgilityPack;
using System.Threading.Tasks;

namespace NetCrawler.Services
{
    public interface IHttpService
    {
        Task<HtmlDocument> Execute(string Url);
    }
}