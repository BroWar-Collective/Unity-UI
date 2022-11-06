using System;

namespace BroWar.UI
{
    public interface IUiFeatureHandler : IDisposable
    {
        void Prepare();
        void Tick();

        bool IsTickable { get; }
    }
}