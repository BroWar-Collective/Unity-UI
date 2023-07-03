using BroWar.UI.Views;
using UnityEngine;

namespace Examples.Handlers
{
    public class CustomViewHandler : ViewsHandler
    {
        [SerializeField]
        private Camera targetCamera;

        protected override ViewsSettings GetDefaultSettings()
        {
            return new ViewsSettings()
            {
                UiCamera = targetCamera
            };
        }
    }
}