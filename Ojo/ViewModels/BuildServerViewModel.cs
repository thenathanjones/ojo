using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Burro;
using Burro.BuildServers;

namespace Ojo.ViewModels
{
    public class BuildServerViewModel : ViewModelBase
    {
        private IEnumerable<PipelineReportViewModel> _pipelineReports;
        private readonly IBuildServer _buildServer;

        public BuildServerViewModel(IBuildServer buildServer)
        {
            _buildServer = buildServer;

            SetupPipelines();
        }

        private void SetupPipelines()
        {
            HandlePipelinesUpdated(_buildServer.PipelineReports);
            _buildServer.PipelinesUpdated += HandlePipelinesUpdated;
        }

        private void HandlePipelinesUpdated(IEnumerable<PipelineReport> reports)
        {
            PipelineReports = reports.Select(p => new PipelineReportViewModel(p));
        }

        public IEnumerable<PipelineReportViewModel> PipelineReports
        {
            get {
                return _pipelineReports;
            }
            private set {
                _pipelineReports = value;
                OnPropertyChanged("PipelineReports");
            }
        }

        public string Type { get { return _buildServer.Config.Type; } }

        public override event PropertyChangedEventHandler PropertyChanged;
    }
}