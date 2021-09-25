using NetCrawler.Models;
using NetCrawler.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NetCrawler.Controllers
{
    public class CrawlerController : Controller
    {
        private ICrawlerService _crawlerService;

        public CrawlerController(
            ICrawlerService crawlerService    
        )
        {
            _crawlerService = crawlerService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SeachUrl(
            CrawlerViewModel model
        )
        {
            if (ModelState.IsValid)
            {
                await _crawlerService.Execute(model);
            }

            return View("Index", model);
        }
    }
}