﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI
{
    //TODO: make it abstract? & initialization method

    /// <inheritdoc cref="IViewsManager"/>
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Views Manager")]
    public class ViewsManager : MonoBehaviour, IViewsManager
    {
        private readonly Dictionary<Type, UiView> viewsByTypes = new Dictionary<Type, UiView>();

        private readonly List<UiView> activeViews = new List<UiView>();

        //TODO: inject main camera
        //TODO: inject tooltip system
        //TODO: inject game references

        [SerializeField]
        private List<UiView> views;

        //TODO: events

        protected virtual void Awake()
        {
            CacheViews();
        }

        private void CacheViews()
        {
            foreach (var view in views)
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
        //TODO: event when view is completly visible

        private void Show(UiView view)
        {
            view.Show();
            activeViews.Add(view);
        }

        private void Hide(UiView view)
        {
            view.Hide();
            activeViews.Remove(view);
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
                Hide(view);
            }
        }

        public void HideActiveViews()
        {
            foreach (var view in activeViews)
            {
                Hide(view);
            }
        }

        /// <inheritdoc cref="IViewsManager"/>
        public IReadOnlyCollection<UiView> Views => viewsByTypes.Values;
        /// <inheritdoc cref="IViewsManager"/>
        public IReadOnlyCollection<UiView> ActiveViews => activeViews;
    }
}