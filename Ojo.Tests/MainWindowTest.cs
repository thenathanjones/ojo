using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using NUnit.Framework;
using Ojo.View;

namespace Ojo.Tests
{
    [TestFixture]
    public class MainWindowTest
    {
        [Test, Ignore("Work out how to unit test the UI components")]
        public void HidesInSystemTrayWhenMinimised()
        {
            var mainWindow = new MainWindow();
            mainWindow.Dispatcher.Invoke(new Action(mainWindow.Show), DispatcherPriority.Normal);
            mainWindow.Show();

            Assert.IsTrue(mainWindow.IsVisible);
        }
    }
}
