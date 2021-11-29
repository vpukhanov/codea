using Codea.View_Models;
using System;
using System.Windows.Input;

namespace Codea.Commands
{
    public class NewDocumentCommand : ICommand
    {
        public DocumentViewModel vm;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vm.Content = "";
        }
    }
}
