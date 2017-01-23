using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebCrawler.ViewModel
{
    public class ClickCommand : ICommand
    {
        private Action action;
        private bool canExecute;
        public event EventHandler CanExecuteChanged;

        public ClickCommand(Action action)
        {
            this.action = action;
            canExecute = true;
        }

        public bool CanExecute(object parametr)
        {
            return canExecute;
        }

        public void Execute(object parametr)
        {
            action();
        }
    }
}
