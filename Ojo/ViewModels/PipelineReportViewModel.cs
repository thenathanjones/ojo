using Burro;

namespace Ojo.ViewModels
{
    public interface IPipelineReportViewModel
    {
        string Name { get; }
        string URL { get; }
        string PipelineState { get; }
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

        public ActionCommand OpenLinkCommand { get; private set; }

        public string PipelineState
        {
            get
            {
                if (_pipelineReport.BuildState == BuildState.Success)
                {
                    if (_pipelineReport.Activity == Activity.Busy)
                    {
                        return "Busy";
                    }
                    else
                    {
                        return "Success";
                    }
                }
                else
                {
                    return "Failure";
                }
            }
        }
    }
}