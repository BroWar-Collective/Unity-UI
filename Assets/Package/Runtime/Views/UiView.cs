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

        public bool IsInitialized { get; private set; }
    }
}