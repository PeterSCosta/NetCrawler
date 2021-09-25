using NetCrawler.Models;
using System.Threading.Tasks;

namespace NetCrawler.Services
{
    public interface ICrawlerService
    {
        Task<CrawlerViewModel> Execute(CrawlerViewModel model);
    }
}