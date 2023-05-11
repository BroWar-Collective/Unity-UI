using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Handlers
{
    using BroWar.Common;
    using BroWar.UI.Elements;
    using BroWar.UI.Management;
    using BroWar.UI.Management.Views;

    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Handlers/Views Handler")]
    public class ViewsHandler : UiHandlerBehaviour, IUiViewsHandler
    {
        protected readonly Dictionary<Type, UiView> viewsByTypes = new Dictionary<Type, UiView>();
        protected readonly List<UiView> activeViews = new List<UiView>();

        [SerializeField, ReorderableList]
        protected List<UiView> views;
        [SerializeField, ReorderableList]
        protected ViewContext[] contexts;

        protected Camera canvasCamera;

        public event Action<UiView> OnShowView;
        public event Action<UiView> OnHideView;
        public event Action OnInitialized;

        //TODO:
        // - what about views between scenes? - no needed
        // - better initialization (better settings?)
        // - handle initial active views (data structure for it?)
        // - namespaces
        // - refactor
        // - can hide and can show
        // - hide/show immedietly

        private void InitializeViews()
        {
            for (var i = 0; i < contexts.Length; i++)
            {
                var context = contexts[i];
                var view = context.view;
                if (view == null)
                {
                    LogHandler.Log($"[UI] {nameof(ViewContext)} at index {i} is invalid.", LogType.Warning);
                    continue;
                }

                var type = view.GetType();
                if (viewsByTypes.ContainsKey(type))
                {
                    LogHandler.Log($"[UI] View ({type.Name}) is cached multiple times.", LogType.Warning);
                    continue;
                }

                viewsByTypes.Add(type, view);
                view.Initialize(canvasCamera);
                if (context.showOnInitialize)
                {
                    ShowInternally(view, true);
                }
                else
                {
                    HideInternally(view, true);
                }
            }
        }

        private void ShowInternally(UiView view, bool immediately)
        {
            if (view == null || (view.IsActive && !view.Hides))
            {
                return;
            }

            view.Show(immediately);
            activeViews.Add(view);
            OnShowView?.Invoke(view);
        }

        private void HideInternally(UiView view, bool immediately)
        {
            if (view == null || (!view.IsActive || view.Hides))
            {
                return;
            }

            view.Hide(immediately);
            if (activeViews.Remove(view))
            {
                OnHideView?.Invoke(view);
            }
        }

        protected virtual ViewsSettings GetDefaultSettings()
        {
            return new ViewsSettings()
            {
                CanvasCamera = Camera.main
            };
        }

        protected virtual void OnInitialize()
        {
            InitializeViews();
        }

        public override void Prepare()
        {
            base.Prepare();
            var settings = GetDefaultSettings();
            Initialize(settings);
        }

        public void Initialize(ViewsSettings settings)
        {
            if (IsInitialized)
            {
                LogHandler.Log($"[UI] {GetType().Name} is already initialized.", LogType.Warning);
                return;
            }

            canvasCamera = settings.CanvasCamera != null
                ? settings.CanvasCamera : Camera.main;
            OnInitialize();
            IsInitialized = true;
            OnInitialized?.Invoke();
        }

        public void Show<T>() where T : UiView
        {
            Show(typeof(T));
        }

        public void Show(Type viewType)
        {
            if (TryGetView(viewType, out var view))
            {
                Show(view);
            }
        }

        public void Show(UiView view)
        {
            ShowInternally(view, false);
        }

        public void Hide<T>() where T : UiView
        {
            Hide(typeof(T));
        }

        public void Hide(Type viewType)
        {
            if (TryGetView(viewType, out var view))
            {
                Hide(view);
            }
        }

        public void Hide(UiView view)
        {
            HideInternally(view, false);
        }

        public bool TryGetView(Type type, out UiView view)
        {
            if (viewsByTypes.TryGetValue(type, out view))
            {
                return view != null;
            }

            view = null;
            return false;
        }

        public bool TryGetView<T>(out T view) where T : UiView
        {
            if (TryGetView(typeof(T), out var cachedView))
            {
                view = cachedView as T;
                return view != null;
            }

            view = null;
            return false;
        }

        public void HideAll()
        {
            foreach (var view in Views)
            {
                if (view.IsActive)
                {
                    HideInternally(view, false);
                }
            }
        }

        public bool IsInitialized { get; private set; }

        /// <inheritdoc cref="IUiViewsHandler"/>
        public IReadOnlyCollection<UiView> Views => viewsByTypes.Values;
        /// <inheritdoc cref="IUiViewsHandler"/>
        public IReadOnlyCollection<UiView> ActiveViews => activeViews;
    }
}