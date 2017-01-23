using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WebCrawler.Model;
using WebCrawlerLibrary;

namespace WebCrawler.ViewModel
{
    public class ViewModelClass : ObservableObject
    {
        private int counter = 0;
        public int Counter
        {
            get
            {
                return counter;
            }
            set
            {
                counter = value;
                OnPropertyChanged(nameof(Counter));
            }
        }
        private ClickCommand clickCommand;
        public ClickCommand ClickCommand
        {
            get
            {
                if (clickCommand == null)
                {
                    clickCommand = new ClickCommand(() => { Counter++; });
                }
                return clickCommand;
            }
        }

        private WebCrawlerModel webCrawlerModel;
        private CrawlResult crawlResult;
        public CrawlResult CrawlResult
        {
            get
            {
                return crawlResult;
            }
            set
            {
                crawlResult = value;
                OnPropertyChanged(nameof(CrawlResult));
            }
        }

        public CrawlCommand CrawlCommand
        {
            get
            {
                return CreateCrawlCommand(webCrawlerModel);
            }
        }

        public ViewModelClass()
        {
            webCrawlerModel = new WebCrawlerModel();
        }

        private CrawlCommand CreateCrawlCommand(WebCrawlerModel webCrawlerModel)
        {
            CrawlCommand crawlCommand = new CrawlCommand(async () =>
            {
                if (SetConfiguration(webCrawlerModel))
                {
                    if (CrawlCommand.CanExecute(null))
                    {
                        CrawlCommand.Disable();
                        CrawlResult = await webCrawlerModel.ExecuteCrawling();
                        CrawlCommand.Enable();
                    }
                }
            });
            return crawlCommand;
        }

        private bool SetConfiguration(WebCrawlerModel webCrawlerModel)
        {
            try
            {
                webCrawlerModel.SetConfigurationData();
                return true;
            }
            catch(Exception ex)
            {
                Logger.Record(ex.Message, string.Empty);
                Info = ex.Message;
                return false;
            }
        }
        
        private string info = string.Empty;
        public string Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                OnPropertyChanged(nameof(info));
            }
        }
    }
}
