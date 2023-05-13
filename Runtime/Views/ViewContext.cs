using System;

namespace BroWar.UI.Views
{
    using BroWar.UI.Elements;

    [Serializable]
    public class ViewContext
    {
        public bool showOnInitialize;
        public UiView view;
    }
}