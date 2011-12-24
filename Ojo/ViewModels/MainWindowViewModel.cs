using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Burro.BuildServers;
using Ojo.View;

namespace Ojo.ViewModels
{
    public interface IMainWindowViewModel
    {
        IEnumerable<BuildServerViewModel> BuildServers { get; }
        ICommand CloseApplicationCommand { get; }
        ICommand MinimizeToTrayCommand { get; }
    }

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        public IEnumerable<BuildServerViewModel> BuildServers { get; private set; }

        public ICommand CloseApplicationCommand { get; private set; }
        public ICommand MinimizeToTrayCommand { get; private set; }

        public MainWindowViewModel(IMainWindow mainWindow, IEnumerable<IBuildServer> buildServers)
        {
            BuildServers = buildServers.Select(b => new BuildServerViewModel(b));

            CloseApplicationCommand = new ActionCommand(() => Application.Current.Shutdown());
            MinimizeToTrayCommand = new ActionCommand(mainWindow.MinimizeToTray);
        }
    }
}
