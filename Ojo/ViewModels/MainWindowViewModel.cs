using System.Collections.Generic;
using System.Linq;
using Burro.BuildServers;

namespace Ojo.ViewModels
{
    public class MainWindowViewModel
    {
        public IEnumerable<BuildServerViewModel> BuildServers { get; private set; }

        public MainWindowViewModel(IEnumerable<IBuildServer> buildServers)
        {
            BuildServers = buildServers.Select(b => new BuildServerViewModel(b));
        }
    }
}
