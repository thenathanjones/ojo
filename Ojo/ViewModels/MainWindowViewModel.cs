using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Burro.BuildServers;

namespace Ojo.ViewModels
{
    public interface IMainWindowViewModel
    {
        IEnumerable<BuildServerViewModel> BuildServers { get; }
        ICommand CloseApplicationCommand { get; }
    }

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public IEnumerable<BuildServerViewModel> BuildServers { get; private set; }

        public ICommand CloseApplicationCommand { get; private set; }

        public MainWindowViewModel(IEnumerable<IBuildServer> buildServers)
        {
            BuildServers = buildServers.Select(b => new BuildServerViewModel(b));

            CloseApplicationCommand = new ActionCommand(() => Application.Current.Shutdown());
        }
    }
}
