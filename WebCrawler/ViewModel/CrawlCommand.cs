using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebCrawlerLibrary;

namespace WebCrawler.ViewModel
{
    public class CrawlCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Func<Task> task;
        private bool isEnabled;

        public CrawlCommand(Func<Task> task)
        {
            this.task = task;
            isEnabled = true;
        }

        public bool CanExecute(object parametr)
        {
            return isEnabled;
        }

        public async void Execute(object parameter)
        {
            try
            {
                await ExecuteAsync();
            }
            catch(NullReferenceException ex)
            {
                Logger.Record(ex.Message, string.Empty);
            }
        }

        private Task ExecuteAsync()
        {
            return task();
        }

        public void Enable()
        {
            isEnabled = true;
        }

        public void Disable()
        {
            isEnabled = false;
        }
    }
}
