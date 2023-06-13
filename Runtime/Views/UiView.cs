using System;

namespace BroWar.UI.Views
{
    using BroWar.Common;

    /// <summary>
    /// Base class for all views.
    /// </summary>
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

        //TODO: move it to UiObject?
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
            return !IsActive || Hides;
        }

        public virtual bool CanHide()
        {
            return IsActive && !Hides;
        }

        public virtual bool Shows => false;
        public virtual bool Hides => false;

        public bool IsInitialized { get; private set; }
    }
}