using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Ojo.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void DispatchAction(Action toDispatch)
        {
            Application.Current.Dispatcher.Invoke(toDispatch, DispatcherPriority.Normal);
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}