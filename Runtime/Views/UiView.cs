using System;

namespace BroWar.UI.Views
{
    using BroWar.Common;

    /// <summary>
    /// Base class for all views.
    /// </summary>
    public abstract class UiView : UiObject, IInitializableWithArgument<ViewData>, IDeinitializable
    {
        public event Action OnInitialized;
        public event Action OnDeinitialized;

        protected virtual void OnInitialize(ViewData data)
        { }

        protected virtual void OnDeinitialize()
        { }

        public virtual void Initialize(ViewData data)
        {
            OnInitialize(data);
            IsInitialized = true;
            OnInitialized?.Invoke();
        }

        public virtual void Deinitialize()
        {
            OnDeinitialize();
            IsInitialized = false;
            OnDeinitialized?.Invoke();
        }

        public bool IsInitialized { get; private set; }
    }
}