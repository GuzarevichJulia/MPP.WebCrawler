using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using WebCrawlerLibrary;

namespace WebCrawler.Config
{
    public class ConfigurationSetter
    {
        private const string DepthTag = "depth";
        private const string RootUrlsTag = "rootUrls";
        private const string UrlValueTag = "urlValue";

        public ConfigurationData Set(string filePath)
        {
            if (File.Exists(filePath))
            {
                XDocument document = document = XDocument.Load(filePath);
                ConfigurationData configurationData = new ConfigurationData();
                try
                {
                    configurationData.Depth = SetDepthValue(document);
                    configurationData.RootUrls = SetRootUrls(document);
                    return configurationData;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new FileNotFoundException("Incorrect value of file path");                
            }
        }

        private int SetDepthValue(XDocument document)
        {
            XElement root = document.Root;
            string depth = null;
            try
            {
                depth = root.Element(DepthTag).Value;
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException("Check tags in configuration file");
            }
            int depthValue;
            if(!int.TryParse(depth, out depthValue))
            {
                throw new ArgumentException("Check the value of analyzed depth");
            }
            if (depthValue > 0)
            {
                return depthValue;
            }
            else
            {
                throw new ArgumentException("Value of analysed depth must be greater than zero");
            }
        }

        private string[] SetRootUrls(XDocument document)
        {
            try
            {
                XElement root = document.Root;
                XElement rootUrls = root.Element(RootUrlsTag);
                List<string> rootUrlsList = new List<string>();
                foreach (XElement url in rootUrls.Elements(UrlValueTag))
                {
                    if (url.Value != string.Empty)
                    {
                        rootUrlsList.Add(url.Value);
                    }
                    else
                    {
                        throw new ArgumentException("Check the input value of urls list");
                    }
                }
                if(rootUrlsList.Count == 0)
                {
                    throw new ArgumentException("Check the input value of urls list");
                }
                return rootUrlsList.ToArray();
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException("Check tags in configuration file");
            }
        }
    }
}
