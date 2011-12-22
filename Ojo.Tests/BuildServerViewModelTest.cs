using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Burro;
using Burro.BuildServers;
using Burro.Config;
using Moq;
using NUnit.Framework;
using Ojo.ViewModels;

namespace Ojo.Tests
{
    [TestFixture]
    public class BuildServerViewModelTest
    {
        private Mock<IBuildServer> _buildServer;
        private BuildServerViewModel _buildServerVM;
        private PipelineReport _pipeline1;
        private PipelineReport _pipeline2;
        private PipelineReport _pipeline3;

        [SetUp]
        public void Setup()
        {
            _buildServer = new Mock<IBuildServer>();
            _pipeline1 = new PipelineReport()
            {
                Activity = Activity.Paused,
                BuildState = BuildState.Success,
                LastBuildTime = DateTime.Now,
                Name = "Billy Bob",
                LinkURL = "http://blah.blah.com"
            };
            _pipeline2 = new PipelineReport()
            {
                Activity = Activity.Busy,
                BuildState = BuildState.Failure,
                LastBuildTime = DateTime.Now,
                Name = "Billy Jane",
                LinkURL = "http://blah.blah.org"
            };
            _pipeline3 = new PipelineReport()
            {
                Activity = Activity.Busy,
                BuildState = BuildState.Success,
                LastBuildTime = DateTime.Now,
                Name = "Billy Thomas",
                LinkURL = "http://blah.blah.net"
            };
        }

        [Test]
        public void ExposesPipelinesAsViewModels()
        {
            _buildServer.Setup(x => x.PipelineReports).Returns(new[] {_pipeline1, _pipeline2, _pipeline3});
            _buildServerVM = new BuildServerViewModel(_buildServer.Object);

            Assert.IsNotNull(_buildServerVM.PipelineReports);
            Assert.AreEqual(3, _buildServerVM.PipelineReports.Count());
            Assert.IsInstanceOf<PipelineReportViewModel>(_buildServerVM.PipelineReports.First());
        }

        [Test]
        public void HasATypeBasedOnServerType()
        {
            var mockConfig = new Mock<IConfig>();
            mockConfig.Setup(x => x.Type).Returns("MonkeyCI");

            _buildServer.Setup(x => x.Config).Returns(mockConfig.Object);
            _buildServerVM = new BuildServerViewModel(_buildServer.Object);
            
            Assert.AreEqual("MonkeyCI", _buildServerVM.Type);
        }
    }
}
