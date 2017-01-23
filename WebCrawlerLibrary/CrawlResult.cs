using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerLibrary
{
    public class CrawlResult
    {
        public string UrlValue { get; set; } 
        public List<CrawlResult> InternalUrls { get; set; }

        public CrawlResult()
        {
            UrlValue = "";
            InternalUrls = new List<CrawlResult>();
        }
    }
}
