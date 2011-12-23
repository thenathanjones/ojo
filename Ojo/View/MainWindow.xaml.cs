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
        public MainWindowViewModel ViewModel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Title = "Ojo : Keep an eye on the build...";
        }

        public void Initialise(IEnumerable<IBuildServer> buildServers)
        {
            ViewModel = new MainWindowViewModel(buildServers);
        }

        private void OnMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
