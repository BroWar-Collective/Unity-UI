using System;

namespace BroWar.UI
{
    //TODO: activity handler
    public interface IShowHideHandler
    {
        void Show(IHidableObject target);
        void Hide(IHidableObject target);
        void Show(IHidableObject target, bool immediately, Action onFinish = null);
        void Hide(IHidableObject target, bool immediately, Action onFinish = null);

        bool Shows { get; }
        bool Hides { get; }
    }
}