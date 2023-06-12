using System;

namespace BroWar.UI
{
    public interface IHidableObject
    {
        event Action OnShow;
        event Action OnHide;

        void SetActive(bool value);
        void Show();
        void Hide();

        bool IsActive { get; }
        bool Shows { get; }
        bool Hides { get; }
    }
}