using UnityEngine;

namespace BroWar.UI.Handlers
{
    using BroWar.UI.Tooltip;

    [AddComponentMenu("BroWar/UI/Handler/Tooltip Handler")]
    public class TooltipHandler : ComponentBasedHandler, ITooltipHandler
    {
        [SerializeField]
        private Canvas canvas;

        public override void Tick()
        {
            //TODO: update tooltips
        }

        public override bool IsTickable => IsTooltipActive;
        public bool IsTooltipActive { get; private set; }
    }
}