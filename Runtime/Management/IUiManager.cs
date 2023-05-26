using System.Collections.Generic;

namespace BroWar.UI.Management
{
    public interface IUiManager
    {
        /// <summary>
        /// Collection of all available <see cref="IUiHandler"/>s.
        /// Each handler is responsible for custom, UI-related feature.
        /// </summary>
        IReadOnlyList<IUiHandler> Handlers { get; }
    }
}