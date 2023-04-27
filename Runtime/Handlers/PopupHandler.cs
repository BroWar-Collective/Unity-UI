using UnityEngine;

namespace BroWar.UI.Handlers
{
    using BroWar.UI.Management;

    [AddComponentMenu("BroWar/UI/Handler/Popup Handler")]
    public class PopupHandler : UiHandlerBehaviour
    {
        [SerializeField]
        private UiViewsManagerBase viewsManager;

        public override bool IsTickable => false;
    }
}