using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Views
{
    using BroWar.Common;

    /// <summary>
    /// Base class for all views.
    /// </summary>
    public abstract class UiView : UiObject, IInitializableWithArgument<ViewData>, IDeinitializable
    {
        [Title("General")]
        [SerializeField, ReorderableList]
        [Tooltip("Optional nested views, usually defined (registered) internally by the custom implementation.")]
        private List<SubViewDefinition> subViews = new List<SubViewDefinition>();

        /// <summary>
        /// <see cref="ViewData"/> used to initialize this view.
        /// </summary>
        private ViewData data;

        public event Action<UiView> OnShowView;
        public event Action<UiView> OnHideView;
        public event Action OnInitialized;
        public event Action OnDeinitialized;

        protected override void OnStartShowing()
        {
            base.OnStartShowing();
            OnShowView?.Invoke(this);
        }

        protected override void OnStopHiding()
        {
            base.OnStopHiding();
            OnHideView?.Invoke(this);
        }

        protected void RegisterSubView(SubViewDefinition definition)
        {
            if (definition == null || definition.view == null)
            {
                LogHandler.Log("[UI][View] Cannot register invalid definition.", LogType.Warning);
                return;
            }

            subViews.Add(definition);
            var view = definition.view;
            if (IsInitialized)
            {
                view.Initialize(data);
            }
        }

        protected virtual void OnInitialize(ViewData data)
        {
            this.data = data;
        }

        protected virtual void OnDeinitialize()
        {
            data = null;
        }

        public override void Show(bool immediately, Action onFinish = null)
        {
            base.Show(immediately, onFinish);
            foreach (SubViewDefinition viewDefinition in subViews)
            {
                if (!viewDefinition.performShowHide)
                {
                    continue;
                }

                UiView view = viewDefinition.view;
                if (view == null || !view.CanShow())
                {
                    continue;
                }

                view.Show(immediately);
            }
        }

        public override void Hide(bool immediately, Action onFinish = null)
        {
            base.Hide(immediately, onFinish);
            foreach (SubViewDefinition viewDefinition in subViews)
            {
                if (!viewDefinition.performShowHide)
                {
                    continue;
                }

                UiView view = viewDefinition.view;
                if (view == null || !view.CanHide())
                {
                    continue;
                }

                view.Hide(immediately);
            }
        }

        public virtual void Initialize(ViewData data)
        {
            OnInitialize(data);
            foreach (SubViewDefinition viewDefinition in subViews)
            {
                UiView view = viewDefinition.view;
                if (view == null)
                {
                    continue;
                }

                view.Initialize(data);
            }

            IsInitialized = true;
            OnInitialized?.Invoke();
        }

        public virtual void Deinitialize()
        {
            OnDeinitialize();
            foreach (SubViewDefinition viewDefinition in subViews)
            {
                UiView view = viewDefinition.view;
                if (view == null)
                {
                    continue;
                }

                view.Deinitialize();
            }

            IsInitialized = false;
            OnDeinitialized?.Invoke();
        }

        /// <inheritdoc cref="IInitializableWithArgument{T}"/>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// Indicates whether this <see cref="UiView"/> or any nested <see cref="UiView"/> is changing it's activity state.
        /// </summary>
        public bool IsTransitioning
        {
            get
            {
                if (IsActivityChanging)
                {
                    return true;
                }

                foreach (SubViewDefinition viewDefinition in subViews)
                {
                    UiView view = viewDefinition.view;
                    if (view.IsTransitioning)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        protected IReadOnlyList<SubViewDefinition> NestedViews => subViews;
    }
}