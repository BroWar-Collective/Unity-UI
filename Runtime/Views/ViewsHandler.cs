using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Views
{
    using BroWar.Common;

    /// <inheritdoc cref="IViewsHandler"/>
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Views/Views Handler")]
    public class ViewsHandler : UiHandlerBehaviour, IViewsHandler
    {
        protected readonly IDictionary<Type, UiView> viewsByTypes = new Dictionary<Type, UiView>();
        protected readonly List<UiView> activeViews = new List<UiView>();

        [SerializeField]
        [Tooltip("Indicates if handler should be initialized during the 'Prepare' callback.")]
        private bool selfInitialize = true;
        [SerializeField, ReorderableList]
        private List<ViewDefinition> definitions;

        protected Camera canvasCamera;
        /// <summary>
        /// Shared <see cref="ViewData"/> used to initialize each associated <see cref="UiView"/>.
        /// </summary>
        protected ViewData viewData;

        public event Action<UiView> OnShowView;
        public event Action<UiView> OnHideView;
        public event Action OnInitialized;

        protected void InitializeViews(ViewData data)
        {
            var definitionsCount = definitions?.Count ?? 0;
            for (var i = 0; i < definitionsCount; i++)
            {
                var definition = definitions[i];
                InitializeView(definition, data);
            }
        }

        protected void DeinitializeViews()
        {
            var definitionsCount = definitions?.Count ?? 0;
            for (var i = 0; i < definitionsCount; i++)
            {
                var definition = definitions[i];
                DeinitializeView(definition);
            }
        }

        protected void InitializeView(ViewDefinition definition, ViewData data)
        {
            var view = definition.view;
            if (view == null)
            {
                LogHandler.Log($"[UI][View] Provided {nameof(ViewDefinition)} is invalid.", LogType.Warning);
                return;
            }

            var type = view.GetType();
            if (ContainsView(type))
            {
                LogHandler.Log($"[UI][View] View ({type.Name}) is cached multiple times.", LogType.Warning);
                return;
            }

            viewsByTypes.Add(type, view);
            view.Initialize(data);
            view.OnShowView += OnShowViewCallback;
            view.OnHideView += OnHideViewCallback;

            var setImmediately = definition.showImmediately;
            if (definition.showOnInitialize)
            {
                if (!setImmediately)
                {
                    HideInternally(view, true, true);
                    ShowInternally(view, false, true);
                }
                else
                {
                    ShowInternally(view, true, true);
                }

                return;
            }

            HideInternally(view, setImmediately, true);
        }

        protected void DeinitializeView(ViewDefinition definition)
        {
            var view = definition.view;
            if (view == null)
            {
                return;
            }

            view.SetActive(false);
            view.Deinitialize();
            view.OnShowView -= OnShowViewCallback;
            view.OnHideView -= OnHideViewCallback;
        }

        protected void ShowInternally(UiView view, bool immediately, bool force = false)
        {
            if (!CanShow(view) && !force)
            {
                return;
            }

            view.Show(immediately);
        }

        protected void HideInternally(UiView view, bool immediately, bool force = false)
        {
            if (!CanHide(view) && !force)
            {
                return;
            }

            view.Hide(immediately);
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
                UiCamera = Camera.main
            };
        }

        protected virtual void OnInitialize()
        {
            InitializeViews(viewData);
        }

        protected void OnShowViewCallback(UiView view)
        {
            activeViews.Add(view);
            OnShowView?.Invoke(view);
        }

        protected void OnHideViewCallback(UiView view)
        {
            if (activeViews.Remove(view))
            {
                OnHideView?.Invoke(view);
            }
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

        public override void Dispose()
        {
            base.Dispose();
            if (!IsInitialized)
            {
                return;
            }

            DeinitializeViews();
            IsInitialized = false;
        }

        public void Initialize(ViewsSettings settings)
        {
            if (IsInitialized)
            {
                LogHandler.Log($"[UI][View] {nameof(ViewsHandler)} is already initialized.", LogType.Warning);
                return;
            }

            canvasCamera = settings.UiCamera;
            viewData = new ViewData()
            {
                UiCamera = canvasCamera
            };

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

        /// <summary>
        /// Registers and add associated <see cref="UiView"/> to the handler.
        /// If <see cref="ViewsHandler"/> is already initialized then <see cref="UiView"/> will also be initialized.
        /// </summary>
        public void RegisterView(ViewDefinition definition)
        {
            if (definition == null || definition.view == null)
            {
                LogHandler.Log("[UI][View] Cannot register invalid definition.", LogType.Warning);
                return;
            }

            definitions.Add(definition);
            if (!IsInitialized)
            {
                return;
            }

            InitializeView(definition, new ViewData()
            {
                UiCamera = canvasCamera
            });
        }

        /// <summary>
        /// Returns <see cref="List{T}"/> of all available <see cref="UiView"/>s.
        /// A new collection is created with each invoke.
        /// </summary>
        public List<UiView> GetAllViews()
        {
            return new List<UiView>(viewsByTypes.Values);
        }

        /// <inheritdoc cref="IInitializable"/>
        public bool IsInitialized { get; private set; }

        /// <inheritdoc cref="IViewsHandler"/>
        public IReadOnlyList<UiView> ActiveViews => activeViews;
    }
}