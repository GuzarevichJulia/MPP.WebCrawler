using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerLibrary
{
    public class Logger
    {
        static public void Record(string record, string url)
        {
            System.Diagnostics.Debug.WriteLine($"{ record} {url}");
        }
    }
}
