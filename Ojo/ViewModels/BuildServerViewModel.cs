using System;
using System.Collections.Generic;
using System.ComponentModel;
using Burro;
using Burro.BuildServers;

namespace Ojo.ViewModels
{
    public class BuildServerViewModel : INotifyPropertyChanged
    {
        private IEnumerable<PipelineReport> _pipelineReports;
        private readonly IBuildServer _buildServer;

        public BuildServerViewModel(IBuildServer buildServer)
        {
            _buildServer = buildServer;

            SetupPipelines();
        }

        private void SetupPipelines()
        {
            PipelineReports = _buildServer.PipelineReports;
            _buildServer.PipelinesUpdated += HandlePipelinesUpdated;
        }

        private void HandlePipelinesUpdated(IEnumerable<PipelineReport> reports)
        {
            PipelineReports = reports;
        }

        public IEnumerable<PipelineReport> PipelineReports
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

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}