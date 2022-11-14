using UnityEngine;

namespace BroWar.UI.Handlers
{
    using BroWar.UI.Tooltip;

    //https://www.youtube.com/watch?v=HXFoUGw7eKk&t=227s&ab_channel=GameDevGuide

    [AddComponentMenu("BroWar/UI/Handler/Tooltip Handler")]
    public class TooltipHandler : UiHandlerBehaviour, ITooltipHandler
    {
        [SerializeField]
        private Canvas canvas;

        //TODO: temporary
        private void Update()
        {
            Tick();
        }

        public override void Tick()
        {
            //TODO: update tooltips
        }

        public override bool IsTickable => IsTooltipActive;
        public bool IsTooltipActive { get; private set; }
    }
}