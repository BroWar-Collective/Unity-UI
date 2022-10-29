using UnityEngine;

namespace BroWar.UI
{
    [AddComponentMenu("BroWar/UI/Tooltip Handler")]
    public class TooltipHandler : UiHandler
    {
        private void Update()
        {
            if (!IsTooltipActive)
            {
                return;
            }

            //TODO: update tooltips
        }

        public bool IsTooltipActive { get; private set; }
    }
}