using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Burro;
using Burro.BuildServers;
using Moq;
using NUnit.Framework;
using Ninject;
using Ojo.View;

namespace Ojo.Tests
{
    public class CoreTest
    {
        private const string DEFAULT_CONFIG_FILE = "./ojo.yml";
        private IKernel _kernel;
        private Mock<IBurroCore> _burro;
        private Mock<IMainWindow> _mainWindow;

        [SetUp]
        public void Setup()
        {
            _kernel = new StandardKernel();
            _burro = new Mock<IBurroCore>();
            _kernel.Bind<IBurroCore>().ToConstant(_burro.Object);

            _mainWindow = new Mock<IMainWindow>();
            _kernel.Bind<IMainWindow>().ToConstant(_mainWindow.Object);

            // make sure there is always a config file for testing
            if (!File.Exists(DEFAULT_CONFIG_FILE))
            {
                File.Copy("Config/mock.yml", DEFAULT_CONFIG_FILE, true);
            }
        }

        [Test]
        public void CreatesConfigFileEditableByAllUsersIfNotPresent()
        {
            if (File.Exists(DEFAULT_CONFIG_FILE))
            {
                File.Delete(DEFAULT_CONFIG_FILE);
            }

            var core = _kernel.Get<OjoCore>();
            try
            {
                core.Initialise();
                Assert.Fail("This should have thrown an exception to force it to close");
            }
            catch (FileLoadException e) { }

            Assert.IsTrue(File.Exists(DEFAULT_CONFIG_FILE));

            var fileSecurity = File.GetAccessControl(DEFAULT_CONFIG_FILE);
            var accessRules = fileSecurity.GetAccessRules(true, false, typeof(System.Security.Principal.NTAccount)).Cast<FileSystemAccessRule>();
            accessRules.First(r => r.AccessControlType == AccessControlType.Allow &&
                              (r.FileSystemRights & FileSystemRights.Write) == FileSystemRights.Write &&
                              r.IdentityReference.Value == "BUILTIN\\Users");

            File.Delete(DEFAULT_CONFIG_FILE);
        }

        [Test]
        public void InitialisationStartsParserWithDefaultConfig()
        {
            var core = _kernel.Get<OjoCore>();
            core.Initialise();
            _burro.Verify(b => b.Initialise(DEFAULT_CONFIG_FILE), Times.Once());
        }

        [Test]
        public void InitialisationAllowsConfigPassedIn()
        {
            var core = _kernel.Get<OjoCore>();
            core.Initialise("test2.yml");
            _burro.Verify(b => b.Initialise("test2.yml"), Times.Once());
        }

        [Test]
        public void InitialisationShowsInitsAndShowsMainWindow()
        {
            var buildServer = new Mock<IBuildServer>();
            _burro.Setup(b => b.BuildServers).Returns(new[] {buildServer.Object});

            var core = _kernel.Get<OjoCore>();
            _mainWindow.Verify(m => m.Show(), Times.Never());
            _mainWindow.Verify(m => m.Initialise(It.IsAny<IEnumerable<IBuildServer>>()), Times.Never());
            core.Initialise();
            _mainWindow.Verify(m => m.Show(), Times.Once());
            _mainWindow.Verify(m => m.Initialise(It.IsAny<IEnumerable<IBuildServer>>()), Times.Once());
        }
    }
}
