using System.Collections.Generic;
using Burro.BuildServers;

namespace Ojo.View
{
    public interface IMainWindow
    {
        void Show();
        void Initialise(IEnumerable<IBuildServer> buildServers);
        void MinimizeToTray();
    }
}