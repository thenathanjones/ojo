using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Burro.BuildServers;
using Moq;
using NUnit.Framework;
using Ojo.ViewModels;

namespace Ojo.Tests.ViewModels
{
    [TestFixture]
    public class MainWindowViewModelTest
    {
        [Test]
        public void ExposesBuildServersAsViewModels()
        {
            var buildServer1 = new Mock<IBuildServer>();
            var buildServer2 = new Mock<IBuildServer>();

            var mainWindowVM = new MainWindowViewModel(new List<IBuildServer>() {buildServer1.Object, buildServer2.Object});
            
            Assert.IsNotNull(mainWindowVM.BuildServers);
            Assert.AreEqual(2, mainWindowVM.BuildServers.Count());
            Assert.IsInstanceOf<BuildServerViewModel>(mainWindowVM.BuildServers.First());
        }
    }
}
