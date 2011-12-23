using System;
using System.Windows.Input;

namespace Ojo.ViewModels
{
    public class ActionCommand : ICommand
    {
        private readonly Action _toExecute;

        public ActionCommand(Action toExecute)
        {
            _toExecute = toExecute;
        }

        public void Execute(object parameter)
        {
            _toExecute.Invoke();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}