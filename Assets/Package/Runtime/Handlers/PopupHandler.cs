using UnityEngine;

namespace BroWar.UI.Handlers
{
    using BroWar.UI.Management;

    [AddComponentMenu("BroWar/UI/Handler/Popup Handler")]
    public class PopupHandler : ComponentBasedHandler
    {
        [SerializeField]
        private ViewsManagerBase viewsManager;

        public override bool IsTickable => false;
    }
}