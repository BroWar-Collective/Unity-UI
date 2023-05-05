using System.Collections.Generic;

namespace BroWar.UI.Management
{
    public interface IUiManager
    {
        IReadOnlyList<IUiHandler> Handlers { get; }
    }
}