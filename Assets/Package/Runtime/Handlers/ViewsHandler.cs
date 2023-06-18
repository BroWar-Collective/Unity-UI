using System.Collections.Generic;
using UnityEngine;
using System;

namespace BroWar.UI.Handlers
{
    using BroWar.Common;
    using BroWar.UI.Elements;
    using BroWar.UI.Management;

    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Handlers/Views Handler")]
    public class ViewsHandler : UiHandlerBehaviour, IUiViewsHandler
    {
        private readonly Dictionary<Type, UiView> viewsByTypes = new Dictionary<Type, UiView>();
        private readonly List<UiView> activeViews = new List<UiView>();

        [SerializeField, ReorderableList]
        private List<UiView> views;

        private Camera canvasCamera;

        public event Action<UiView> OnShowView;
        public event Action<UiView> OnHideView;

        //TODO:
        // - events
        // - what about views between scenes?
        // - better initialization (better settings?)
        // - handle initial active views (data structure for it?)

        private void InitializeViews()
        {
            foreach (var view in views)
            {
                if (view == null)
                {
                    return;
                }

                var type = view.GetType();
                if (viewsByTypes.ContainsKey(type))
                {
                    LogHandler.Log($"[UI] View ({type.Name}) is cached multiple times.", LogType.Warning);
                    return;
                }

                viewsByTypes.Add(type, view);
                view.Initialize(canvasCamera);
            }
        }

        private void ShowInternally(UiView view)
        {
            if (view == null)
            {
                return;
            }

            view.Show();
            activeViews.Add(view);
            OnShowView?.Invoke(view);
        }

        private void HideInternally(UiView view)
        {
            if (view == null)
            {
                return;
            }

            view.Hide();
            if (activeViews.Remove(view))
            {
                OnHideView?.Invoke(view);
            }
        }

        protected virtual void OnInitialize()
        {
            InitializeViews();
        }

        //TODO: temporary solution
        public override void Prepare()
        {
            base.Prepare();
            Initialize();
        }

        public void Initialize()
        {
            Initialize(Camera.main);
        }

        public void Initialize(Camera canvasCamera)
        {
            if (IsInitialized)
            {
                LogHandler.Log($"[UI] {GetType().Name} is already initialized.", LogType.Warning);
                return;
            }

            this.canvasCamera = canvasCamera;
            OnInitialize();
            IsInitialized = true;
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
            ShowInternally(view);
        }

        public void Hide<T>() where T : UiView
        {
            Hide(typeof(T));
        }

        public void Hide(Type viewType)
        {
            if (TryGetView(viewType, out var view))
            {
                HideInternally(view);
            }
        }

        public void Hide(UiView view)
        {
            HideInternally(view);
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
                HideInternally(view);
            }
        }

        public void HideActiveViews()
        {
            foreach (var view in activeViews)
            {
                HideInternally(view);
            }
        }

        public bool IsInitialized { get; private set; }

        /// <inheritdoc cref="IViewsManager"/>
        public IReadOnlyCollection<UiView> Views => viewsByTypes.Values;
        /// <inheritdoc cref="IViewsManager"/>
        public IReadOnlyCollection<UiView> ActiveViews => activeViews;
    }
}