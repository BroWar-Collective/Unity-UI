using System;

namespace BroWar.UI.Common
{
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
            target.Show();
            onFinish?.Invoke();
        }

        public void Hide(IActivityTarget target, bool immediately, Action onFinish = null)
        {
            target.Hide();
            onFinish?.Invoke();
        }

        public void Dispose()
        { }

        public bool Shows => false;
        public bool Hides => false;
    }
}