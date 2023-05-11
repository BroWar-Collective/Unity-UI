using BroWar.UI.Handlers;
using BroWar.UI.Management;
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
                CanvasCamera = targetCamera
            };
        }
    }
}