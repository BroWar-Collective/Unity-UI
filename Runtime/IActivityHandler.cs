using System;

namespace BroWar.UI
{
    public interface IActivityHandler
    {
        void Show(IActivityTarget target);
        void Hide(IActivityTarget target);
        void Show(IActivityTarget target, bool immediately, Action onFinish = null);
        void Hide(IActivityTarget target, bool immediately, Action onFinish = null);

        bool Shows { get; }
        bool Hides { get; }
    }
}