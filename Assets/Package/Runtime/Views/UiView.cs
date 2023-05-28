using System;

namespace BroWar.UI.Views
{
    using BroWar.Common;

    //TODO: interface for hidable objects (in time and immediate)
    public abstract class UiView : UiObject, IInitializableWithArgument<ViewData>
    {
        public event Action OnInitialized;

        protected virtual void OnInitialize(ViewData data)
        { }

        public virtual void Initialize(ViewData data)
        {
            OnInitialize(data);
            IsInitialized = true;
            OnInitialized?.Invoke();
        }

        public virtual void Show(bool immediately, Action onFinish = null)
        {
            base.Show();
            onFinish?.Invoke();
        }

        public virtual void Hide(bool immediately, Action onFinish = null)
        {
            base.Hide();
            onFinish?.Invoke();
        }

        public virtual bool CanShow()
        {
            return !IsActive;
        }

        public virtual bool CanHide()
        {
            return IsActive;
        }

        public bool IsInitialized { get; private set; }
    }
}