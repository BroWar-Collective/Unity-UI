using System;

namespace BroWar.UI
{
    public interface IActivityTarget
    {
        event Action OnShow;
        event Action OnHide;

        void SetActive(bool value);

        bool IsActive { get; }
    }
}