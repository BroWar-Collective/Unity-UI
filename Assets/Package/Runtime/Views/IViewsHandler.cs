using System;
using System.Collections.Generic;

namespace BroWar.UI.Views
{
    using BroWar.Common;

    public interface IViewsHandler : IInitializableWithArgument<ViewsSettings>
    {
        event Action<UiView> OnShowView;
        event Action<UiView> OnHideView;

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

        /// <summary>
        /// Returns <see cref="List{T}"/> of all available <see cref="UiView"/>s.
        /// A new collection is created with each invoke.
        /// </summary>
        List<UiView> GetAllViews();

        /// <summary>
        /// Hides all <see cref="UiView"/>s.
        /// </summary>
        void HideAll();
        /// <summary>
        /// Shows all <see cref="UiView"/>s.
        /// </summary>
        void ShowAll();

        /// <summary>
        /// Registers and add associated <see cref="UiView"/> to the handler.
        /// If <see cref="ViewsHandler"/> is already initialized then <see cref="UiView"/> will also be initialized.
        /// </summary>
        void RegisterView(ViewDefinition definition);

        IReadOnlyList<UiView> ActiveViews { get; }
    }
}