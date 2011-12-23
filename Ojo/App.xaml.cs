using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Burro;
using Burro.Util;
using Ninject;

namespace Ojo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private OjoCore _core;
        private IKernel _kernel;

        public App()
        {
            LoadCore();

            LoadMainWindow();
        }

        private void LoadMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void LoadCore()
        {
            ConfigureBindings();

            _core = _kernel.Get<OjoCore>();
        }

        private void ConfigureBindings()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<ITimer>().ToConstant(new TimersTimer(new TimeSpan(0, 0, 5)));

            _kernel.Bind<IBurroCore>().To<BurroCore>();
        }
    }
}
