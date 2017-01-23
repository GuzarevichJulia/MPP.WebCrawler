using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Controls;
using WebCrawlerLibrary;

namespace WebCrawler.Convertation
{
    public class ResultConverter : IValueConverter
    {
        public object Convert(object value, Type type, object parameter, CultureInfo cultureInfo)
        {            
            CrawlResult crawlResult = (CrawlResult)value;
            if (value != null)
            {
                List<TreeViewItem> resultList = new List<TreeViewItem>();
                resultList.Add(ConvertToTree(crawlResult));
                return resultList;
            }
            else
            {
                return new object();
            }       
        }

        private TreeViewItem ConvertToTree(CrawlResult crawlResult)
        {
            TreeViewItem root = CreateTreeItem(crawlResult.UrlValue);
            foreach(CrawlResult url in crawlResult.InternalUrls)
            {
                TreeViewItem treeViewItem = ConvertToTree(url);
                root.Items.Add(treeViewItem);
            }
            return root;
        }

        private TreeViewItem CreateTreeItem(string urlValue)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = urlValue;
            return item;
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
