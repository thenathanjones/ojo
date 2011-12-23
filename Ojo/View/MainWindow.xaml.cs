using System.Collections.Generic;
using System.Windows;
using Burro.BuildServers;
using Ojo.ViewModels;

namespace Ojo.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Initialise(IEnumerable<IBuildServer> buildServers)
        {
            ViewModel = new MainWindowViewModel(buildServers);
        }

        protected MainWindowViewModel ViewModel { get; private set; }
    }
}
