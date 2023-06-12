using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Views
{
    using BroWar.Common;

    /// <inheritdoc cref="IUiViewsHandler"/>
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Views/Views Handler")]
    public class ViewsHandler : UiHandlerBehaviour, IUiViewsHandler
    {
        protected readonly Dictionary<Type, UiView> viewsByTypes = new Dictionary<Type, UiView>();
        protected readonly List<UiView> activeViews = new List<UiView>();

        [SerializeField]
        [Tooltip("Indicates if handler should be initialized during the 'Prepare' callback.")]
        protected bool selfInitialize = true;
        [SerializeField, ReorderableList]
        protected ViewContext[] contexts;

        protected Camera canvasCamera;

        public event Action<UiView> OnShowView;
        public event Action<UiView> OnHideView;
        public event Action OnInitialized;

        private void InitializeViews()
        {
            var data = new ViewData()
            {
                CanvasCamera = canvasCamera
            };

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
                if (ContainsView(type))
                {
                    LogHandler.Log($"[UI] View ({type.Name}) is cached multiple times.", LogType.Warning);
                    continue;
                }

                viewsByTypes.Add(type, view);
                view.Initialize(data);
                if (context.showOnInitialize)
                {
                    ShowInternally(view, true, true);
                }
                else
                {
                    HideInternally(view, true, true);
                }
            }
        }

        protected void ShowInternally(UiView view, bool immediately, bool force = false)
        {
            if (!CanShow(view) && !force)
            {
                return;
            }

            view.Show(immediately);
            activeViews.Add(view);
            OnShowView?.Invoke(view);
        }

        protected void HideInternally(UiView view, bool immediately, bool force = false)
        {
            if (!CanHide(view) && !force)
            {
                return;
            }

            view.Hide(immediately);
            if (activeViews.Remove(view))
            {
                OnHideView?.Invoke(view);
            }
        }

        protected virtual bool CanShow(UiView view)
        {
            return view != null && view.CanShow();
        }

        protected virtual bool CanHide(UiView view)
        {
            return view != null && view.CanHide();
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
            if (selfInitialize)
            {
                var settings = GetDefaultSettings();
                Initialize(settings);
            }
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
            Show(view, false);
        }

        public void Show(UiView view, bool immediately)
        {
            ShowInternally(view, immediately);
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
            Hide(view, false);
        }

        public void Hide(UiView view, bool immediately)
        {
            HideInternally(view, immediately);
        }

        public bool ContainsView<T>() where T : UiView
        {
            return ContainsView(typeof(T));
        }

        public bool ContainsView(Type type)
        {
            return viewsByTypes.ContainsKey(type);
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
            var views = GetAllViews();
            foreach (var view in views)
            {
                if (view.IsActive)
                {
                    HideInternally(view, false);
                }
            }
        }

        public void ShowAll()
        {
            var views = GetAllViews();
            foreach (var view in views)
            {
                ShowInternally(view, false);
            }
        }

        public List<UiView> GetAllViews()
        {
            return new List<UiView>(viewsByTypes.Values);
        }

        public bool IsInitialized { get; private set; }

        /// <inheritdoc cref="IUiViewsHandler"/>
        public IReadOnlyCollection<UiView> ActiveViews => activeViews;
    }
}