using System;

namespace BroWar.UI
{
    public interface IActivityHandler
    {
        void Show(IHidableObject target);
        void Hide(IHidableObject target);
        void Show(IHidableObject target, bool immediately, Action onFinish = null);
        void Hide(IHidableObject target, bool immediately, Action onFinish = null);

        bool Shows { get; }
        bool Hides { get; }
    }
}