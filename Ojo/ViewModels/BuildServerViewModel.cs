using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Burro;
using Burro.BuildServers;

namespace Ojo.ViewModels
{
    public interface IBuildServerViewModel
    {
        ObservableCollection<PipelineReportViewModel> PipelineReports { get; }
        string Type { get; }
    }

    public class BuildServerViewModel : ViewModelBase, IBuildServerViewModel
    {
        private readonly IBuildServer _buildServer;

        public BuildServerViewModel(IBuildServer buildServer)
        {
            _buildServer = buildServer;

            SetupPipelines();
        }

        private void SetupPipelines()
        {
            PipelineReports = new ObservableCollection<PipelineReportViewModel>();

            _buildServer.PipelinesUpdated += HandlePipelinesUpdated;
        }

        private void HandlePipelinesUpdated(IEnumerable<PipelineReport> reports)
        {
            var newReports = reports.Select(p => new PipelineReportViewModel(p));

            var update = new Action(() =>
                                        {
                                            PipelineReports.Clear();

                                            foreach (var pipelineReportViewModel in newReports)
                                            {
                                                PipelineReports.Add(pipelineReportViewModel);
                                            }
                                        });

            DispatchAction(update);
        }

        public ObservableCollection<PipelineReportViewModel> PipelineReports { get; private set; }

        public string Type { get { return _buildServer.Config.Type; } }

        public override event PropertyChangedEventHandler PropertyChanged;
    }
}