using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Config;
using WebCrawlerLibrary;

namespace WebCrawler.Model
{
    public class WebCrawlerModel
    {
        private const string ConfigFilePath = "..\\..\\Config\\Configuration.xml";
        private ConfigurationData configData;

        public async Task<CrawlResult> ExecuteCrawling()
        {
            using (SimpleWebCrawler webCrawler = new SimpleWebCrawler())
            {
                return await webCrawler.PerformCrawlingAsync(configData.RootUrls, configData.Depth);
            }
        }

        public void SetConfigurationData()
        {
            ConfigurationSetter configSetter = new ConfigurationSetter();
            configData = configSetter.Set(ConfigFilePath);
        }
    }
}
