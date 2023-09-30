using System;

namespace BroWar.UI.Common
{
    /// <summary>
    /// Basic <see cref="IActivityHandler"/> that shows and hides targets without any time-based operations.
    /// </summary>
    [Serializable]
    public class StandardActivityHandler : IActivityHandler
    {
        public void Show(IActivityTarget target)
        {
            Show(target, false);
        }

        public void Hide(IActivityTarget target)
        {
            Hide(target, false);
        }

        public void Show(IActivityTarget target, bool immediately, Action onFinish = null)
        {
            target.SetActive(true);
            onFinish?.Invoke();
        }

        public void Hide(IActivityTarget target, bool immediately, Action onFinish = null)
        {
            target.SetActive(false);
            onFinish?.Invoke();
        }

        public void Dispose()
        { }

        public bool Shows => false;
        public bool Hides => false;
    }
}