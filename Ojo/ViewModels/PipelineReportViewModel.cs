using System;
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

        public string LastBuildTime
        {
            get
            {
                var delta = (DateTime.Now - _pipelineReport.LastBuildTime);

                if (delta.TotalSeconds < 60)
                {
                    return "under a minute ago";
                }
                if (delta.TotalSeconds < 120)
                {
                    return "a minute ago";
                }
                if (delta.TotalMinutes < 60)
                {
                    var minutes = (int)(delta.TotalMinutes - (delta.TotalMinutes % 5));
                    return "about " + minutes + " minutes ago";
                }
                if (delta.TotalHours < 2)
                {
                    return "about an hour ago";
                }
                if (delta.TotalHours < 24)
                {
                    return "about " + (int)delta.TotalHours + " hours ago";
                }
                return "over " + (int)delta.TotalDays + " days ago";
            }
        }
    }
}