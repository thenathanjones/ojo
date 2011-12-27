using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Burro;
using NUnit.Framework;
using Ojo.ViewModels;

namespace Ojo.Tests.ViewModels
{
    [TestFixture]
    public class PipelineReportViewModelTest
    {
        private PipelineReport _pipeline;

        [SetUp]
        public void Setup()
        {
            _pipeline = new PipelineReport()
                            {
                                Activity = Activity.Busy,
                                BuildState = BuildState.Success,
                                LastBuildTime = DateTime.Now,
                                LinkURL = "http://ci:8153/go/pipelines/jimbo",
                                Name = "CI-Trunk"
                            };
        }

        [Test]
        public void ExposesName()
        {
            var pipelineReportVM = new PipelineReportViewModel(_pipeline);
            
            Assert.AreEqual("CI-Trunk", pipelineReportVM.Name);
        }

        [Test]
        public void ExposesURL()
        {
            var pipelineReportVM = new PipelineReportViewModel(_pipeline);

            Assert.AreEqual("http://ci:8153/go/pipelines/jimbo", pipelineReportVM.URL);
        }        

        [Test]
        public void CanShowSuccessfulBuild()
        {
            var successfulPipeline = new PipelineReport()
                                         {
                                             Activity = Activity.Idle,
                                             BuildState = BuildState.Success,
                                             LastBuildTime = DateTime.Now,
                                             LinkURL = "http://ci:8153/go/pipelines/jimbo",
                                             Name = "CI-Trunk"
                                         };

            var pipelineReportVM = new PipelineReportViewModel(successfulPipeline);
            Assert.AreEqual("Success", pipelineReportVM.PipelineState);
        }

        [Test]
        public void CanShowFailedBuild()
        {
            var failedPipeline = new PipelineReport()
            {
                Activity = Activity.Idle,
                BuildState = BuildState.Failure,
                LastBuildTime = DateTime.Now,
                LinkURL = "http://ci:8153/go/pipelines/jimbo",
                Name = "CI-Trunk"
            };

            var pipelineReportVM = new PipelineReportViewModel(failedPipeline);
            Assert.AreEqual("Failure", pipelineReportVM.PipelineState);
        }

        [Test]
        public void CanShowBusyBuild()
        {
            var busyPipeline = new PipelineReport()
            {
                Activity = Activity.Busy,
                BuildState = BuildState.Success,
                LastBuildTime = DateTime.Now,
                LinkURL = "http://ci:8153/go/pipelines/jimbo",
                Name = "CI-Trunk"
            };

            var pipelineReportVM = new PipelineReportViewModel(busyPipeline);
            Assert.AreEqual("Busy", pipelineReportVM.PipelineState);
        }

        public void ExposesCommandToOpenLinkInBrowser()
        {
            var pipelineReportVM = new PipelineReportViewModel(_pipeline);

            Assert.IsNotNull(pipelineReportVM.OpenLinkCommand);
            Assert.IsInstanceOf<ActionCommand>(pipelineReportVM.OpenLinkCommand);
        }
    }
}
