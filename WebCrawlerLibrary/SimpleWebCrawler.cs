using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;

namespace WebCrawlerLibrary
{
    public class SimpleWebCrawler : ISimpleWebCrawler, IDisposable
    {
        List<CrawlResult> crawlResultList;
        private int analysisDepth;
        HttpClient httpClient;
        public int AnalysisDepth
        {
            get
            {
                return analysisDepth;
            }
            set
            {
                if (value <= 6)
                {
                    analysisDepth = value;
                }
                else
                {
                    analysisDepth = 6;
                }

            }
        }

        public SimpleWebCrawler()
        {
            httpClient = new HttpClient();
        }

        public async Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls, int depth)
        {
            AnalysisDepth = depth;
            CrawlResult topCrawlResult = new CrawlResult();
            topCrawlResult.UrlValue = "Result:";
            crawlResultList = Initialize(rootUrls);
            topCrawlResult.InternalUrls = crawlResultList;
            await CrawlUrls(topCrawlResult.InternalUrls, AnalysisDepth);            
            return topCrawlResult;
        }

        private List<CrawlResult> Initialize(string[] rootUrls)
        {
            List<CrawlResult> crawlResultList = new List<CrawlResult>();
            foreach (string urlAdressItem in rootUrls)
            {
                CrawlResult crawlResultItem = new CrawlResult();
                crawlResultItem.UrlValue = urlAdressItem;
                crawlResultList.Add(crawlResultItem);
            }
            return crawlResultList;
        }

        private async Task CrawlUrls(List<CrawlResult> urlList, int depth)
        {
            foreach (CrawlResult urlItem in urlList)
            {
                List<CrawlResult> internalUrls = await GetInternalUrl(urlItem.UrlValue);
                urlItem.InternalUrls = internalUrls;
                if ((depth > 1) && (urlItem.InternalUrls != null))
                {
                    await CrawlUrls(internalUrls, depth - 1);
                }
            }            
        }

        private async Task<List<CrawlResult>> GetInternalUrl(string url)
        {
            List<CrawlResult> internalUrlList = null;
            string html = await LoadPage(url);
            if (html != null)
            {
                internalUrlList = FindInternalUrls(html);
            }
            return internalUrlList;
        }

        private async Task<string> LoadPage(string url)
        {
            try
            {
                return await httpClient.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                Logger.Record(ex.Message, url);
                return null;
            }
        }

        private List<CrawlResult> FindInternalUrls(string html)
        {
            List<CrawlResult> internalUrlList = new List<CrawlResult>();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            HtmlNodeCollection htmlNodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
            if (htmlNodes != null)
            {
                foreach (HtmlNode node in htmlNodes)
                {
                    string link = node.Attributes["href"].Value;
                    if(link.StartsWith("https://") || link.StartsWith("http://"))
                    {
                        CrawlResult internalUrlItem = new CrawlResult();
                        internalUrlItem.UrlValue = link;
                        internalUrlList.Add(internalUrlItem);
                    }
                }
            }
            return internalUrlList;
        }
        
        public void Dispose()
        {
            if(httpClient != null)
            {
                httpClient.Dispose();
            }
        }
    }
}
