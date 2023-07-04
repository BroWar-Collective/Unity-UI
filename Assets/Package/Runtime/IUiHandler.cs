using System;

namespace BroWar.UI
{
    using BroWar.Common;

    public interface IUiHandler : IDisposable, ITickable
    {
        void Prepare();

        bool IsTickable { get; }
    }
}