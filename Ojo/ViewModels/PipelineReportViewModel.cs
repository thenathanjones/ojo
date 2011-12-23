using Burro;

namespace Ojo.ViewModels
{
    public interface IPipelineReportViewModel
    {
        string Name { get; }
        string URL { get; }
        string Activity { get; }
        string BuildState { get; }
        ActionCommand OpenLinkCommand { get; }
    }

    public class PipelineReportViewModel : ViewModelBase, IPipelineReportViewModel
    {
        private readonly PipelineReport _pipelineReport;

        public PipelineReportViewModel(PipelineReport pipelineReport)
        {
            _pipelineReport = pipelineReport;
            OpenLinkCommand = new ActionCommand(() => System.Diagnostics.Process.Start(URL));
        }

        public string Name
        {
            get { return _pipelineReport.Name; }
        }

        public string URL
        {
            get { return _pipelineReport.LinkURL; }
        }

        public string Activity
        {
            get { return _pipelineReport.Activity.ToString(); }
        }

        public string BuildState
        {
            get { return _pipelineReport.BuildState.ToString(); }
        }

        public ActionCommand OpenLinkCommand { get; private set; }
    }
}