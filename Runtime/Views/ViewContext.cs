using System;

namespace BroWar.UI.Views
{
    [Serializable]
    public class ViewContext
    {
        public bool showOnInitialize;
        public bool immediateAction;
        public UiView view;
    }
}