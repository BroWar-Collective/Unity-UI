using System;

namespace BroWar.UI.Views
{
    /// <summary>
    /// Definition used to pre-initialize <see cref="UiView"/>s.
    /// </summary>
    [Serializable]
    public class ViewDefinition
    {
        public bool showOnInitialize;
        public bool showImmediately;
        public UiView view;
    }
}