using NetCrawler.Models;
using NetCrawler.Services;
using System;
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
                try
                {
                    await _crawlerService.Execute(model);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }

            return View("Index", model);
        }

    }
}