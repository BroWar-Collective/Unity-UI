using System;
using System.Collections.Generic;

namespace BroWar.UI.Management
{
    using BroWar.UI.Elements;

    /// <summary>
    /// TODO
    /// </summary>
    public interface IViewsManager
    {
        void Show<T>() where T : UiView;
        void Show(Type viewType);
        void Show(UiView view);
        void Hide<T>() where T : UiView;
        void Hide(Type viewType);
        void Hide(UiView view);
        bool TryGetView(Type type, out UiView view);
        bool TryGetView<T>(out T view) where T : UiView;
        void HideAll();
        void HideActiveViews();

        IReadOnlyCollection<UiView> Views { get; }
        IReadOnlyCollection<UiView> ActiveViews { get; }
    }
}