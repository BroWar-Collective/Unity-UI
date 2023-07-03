using System;

namespace BroWar.UI.Views
{
    /// <summary>
    /// Context used to pre-initialize <see cref="UiView"/>s.
    /// </summary>
    [Serializable]
    public class ViewContext
    {
        public bool showOnInitialize;
        public bool showImmediately;
        public UiView view;
    }
}