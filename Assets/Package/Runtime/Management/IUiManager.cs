using System.Collections.Generic;

namespace BroWar.UI.Management
{
    using BroWar.Common;

    /// <summary>
    /// Manager responsible for maintaining UI-related features.
    /// </summary>
    public interface IUiManager : IInitializable, IDeinitializable
    {
        /// <summary>
        /// Collection of all available <see cref="IUiHandler"/>s.
        /// Each handler is responsible for custom, UI-related feature.
        /// </summary>
        IReadOnlyList<IUiHandler> Handlers { get; }
    }
}