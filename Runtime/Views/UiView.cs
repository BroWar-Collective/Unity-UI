using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BroWar.UI.Views
{
    using BroWar.Common;

    /// <summary>
    /// Base class for all views.
    /// </summary>
    public abstract class UiView : UiObject, IInitializableWithArgument<ViewData>, IDeinitializable
    {
        [Title("General")]
        [SerializeField, ReorderableList, FormerlySerializedAs("subViews")]
        [Tooltip("Optional nested views, usually defined (registered) internally by the custom implementation.")]
        private List<SubViewDefinition> predefinedSubViews = new List<SubViewDefinition>();
        [SerializeField, ReorderableList, Disable]
        private List<SubViewDefinition> registeredSubViews;

        private bool isPrewarmed;
        /// <summary>
        /// <see cref="ViewData"/> used to initialize this view.
        /// </summary>
        private ViewData data;

        public event Action<UiView, bool> OnShowView;
        public event Action<UiView, bool> OnHideView;
        public event Action OnInitialized;
        public event Action OnDeinitialized;

        private void PrewarmSubViews()
        {
            if (isPrewarmed)
            {
                return;
            }

            registeredSubViews = new List<SubViewDefinition>(predefinedSubViews);
            isPrewarmed = true;
        }

        protected void RegisterSubView(SubViewDefinition definition)
        {
            if (definition == null || definition.view == null)
            {
                LogHandler.Log("[UI][Views] Cannot register invalid definition.", LogType.Warning);
                return;
            }

            registeredSubViews.Add(definition);
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

        protected virtual void OnUpdateData(ViewData data)
        { }

        public override void Show(bool immediately, Action onFinish = null)
        {
            base.Show(immediately, onFinish);
            OnShowView?.Invoke(this, immediately);
            foreach (var viewDefinition in SubViews)
            {
                if (!viewDefinition.performShowHide)
                {
                    continue;
                }

                var view = viewDefinition.view;
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
            OnHideView?.Invoke(this, immediately);
            foreach (var viewDefinition in SubViews)
            {
                if (!viewDefinition.performShowHide)
                {
                    continue;
                }

                var view = viewDefinition.view;
                if (view == null || !view.CanHide())
                {
                    continue;
                }

                view.Hide(immediately);
            }
        }

        public virtual void Initialize(ViewData data)
        {
            if (IsInitialized || IsInitializing)
            {
                LogHandler.Log($"[UI][Views] {nameof(UiView)} is already initialized.", LogType.Warning);
                return;
            }

            IsInitializing = true;
            PrewarmSubViews();
            OnInitialize(data);
            IsInitializing = false;
            foreach (var viewDefinition in SubViews)
            {
                var view = viewDefinition.view;
                if (view == null || view.IsInitialized)
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
            foreach (var viewDefinition in SubViews)
            {
                var view = viewDefinition.view;
                if (view == null)
                {
                    continue;
                }

                view.Deinitialize();
            }

            registeredSubViews = null;
            isPrewarmed = false;
            IsInitialized = false;
            OnDeinitialized?.Invoke();
        }

        public virtual void UpdateData(ViewData data)
        {
            if (!IsInitialized)
            {
                return;
            }

            this.data = data;
            OnUpdateData(data);
            foreach (var viewDefinition in SubViews)
            {
                var view = viewDefinition.view;
                if (view == null || view.IsInitialized)
                {
                    continue;
                }

                view.UpdateData(data);
            }
        }

        /// <inheritdoc cref="IInitializableWithArgument{T}"/>
        public bool IsInitialized { get; private set; }

        public bool IsInitializing { get; private set; }

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

                foreach (var viewDefinition in SubViews)
                {
                    var view = viewDefinition.view;
                    if (view.IsTransitioning)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public IReadOnlyList<SubViewDefinition> SubViews
        {
            get
            {
                PrewarmSubViews();
                return registeredSubViews;
            }
        }
    }
}