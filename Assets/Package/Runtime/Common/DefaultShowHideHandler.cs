using System;

namespace BroWar.UI.Common
{
    [Serializable]
    public class DefaultShowHideHandler : IShowHideHandler
    {
        public void Show(IHidableObject target)
        {
            Show(target, false);
        }

        public void Hide(IHidableObject target)
        {
            Hide(target, false);
        }

        public void Show(IHidableObject target, bool immediately, Action onFinish = null)
        {
            target.Show();
            onFinish?.Invoke();
        }

        public void Hide(IHidableObject target, bool immediately, Action onFinish = null)
        {
            target.Hide();
            onFinish?.Invoke();
        }

        public bool Shows => false;
        public bool Hides => false;
    }
}