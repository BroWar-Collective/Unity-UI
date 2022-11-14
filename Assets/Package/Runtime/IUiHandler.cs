using System;

namespace BroWar.UI
{
    public interface IUiHandler : IDisposable
    {
        void Prepare();
        void Tick();

        bool IsTickable { get; }
    }
}