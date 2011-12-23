using System.ComponentModel;

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

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}