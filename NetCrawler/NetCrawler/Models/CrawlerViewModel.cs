using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCrawler.Models
{
    public class CrawlerViewModel
    {
        [Required(ErrorMessage = "Website's url is required")]
        public string Url { get; set; }
        public IEnumerable<ImageModel> Images { get; set; }
        public IEnumerable<CommomWordModel> CommomWords { get; set; }
    }
}