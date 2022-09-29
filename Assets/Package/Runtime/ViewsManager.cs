using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Views Manager")]
    public class ViewsManager : MonoBehaviour, IViewsManager
    {
        private readonly Dictionary<Type, UiView> viewsByTypes = new Dictionary<Type, UiView>();

        //TODO: better names

        [SerializeField]
        private List<UiView> predfinedViews;
        [SerializeField]
        private List<UiView> prewarmedViews;

        //TODO: events

        protected virtual void Awake()
        {
            CacheViews();
        }

        private void CacheViews()
        {
            foreach (var view in prewarmedViews)
            {
                CacheView(view);
            }
        }

        private void CacheView(UiView view)
        {
            if (view == null)
            {
                return;
            }

            var type = view.GetType();
            if (viewsByTypes.ContainsKey(type))
            {
                Debug.LogWarning($"[UI] View ({type.Name}) is cached multiple times.");
                return;
            }

            viewsByTypes.Add(type, view);
        }

        //TODO: move public API to interface

        public T Show<T>() where T : UiView
        {
            if (TryGetView<T>(out var view))
            {
                view.Show();
            }

            return view;
        }

        public UiView Show(Type viewType)
        {
            if (TryGetView(viewType, out var view))
            {
                view.Show();
            }

            return view;
        }

        public T Hide<T>() where T : UiView
        {
            if (TryGetView<T>(out var view))
            {
                view.Hide();
            }

            return view;
        }

        public UiView Hide(Type viewType)
        {
            if (TryGetView(viewType, out var view))
            {
                view.Hide();
            }

            return view;
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
                view.Hide();
            }
        }

        public IReadOnlyCollection<UiView> Views => viewsByTypes.Values;
    }
}