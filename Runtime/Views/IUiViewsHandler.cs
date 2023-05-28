using System;
using System.Collections.Generic;

namespace BroWar.UI.Views
{
    using BroWar.Common;

    public interface IUiViewsHandler : IInitializableWithArgument<ViewsSettings>
    {
        void Show<T>() where T : UiView;
        void Show(Type viewType);
        void Show(UiView view);
        void Show(UiView view, bool immediately);
        void Hide<T>() where T : UiView;
        void Hide(Type viewType);
        void Hide(UiView view);
        void Hide(UiView view, bool immediately);
        bool ContainsView<T>() where T : UiView;
        bool ContainsView(Type type);
        bool TryGetView(Type type, out UiView view);
        bool TryGetView<T>(out T view) where T : UiView;
        List<UiView> GetAllViews();
        void HideAll();
        void ShowAll();

        IReadOnlyCollection<UiView> ActiveViews { get; }
    }
}