using TMPro;
using UnityEngine;

namespace BroWar.UI.Tooltip
{
    public class TooltipBehaviour : UiObject
    {
        private ITooltipHandler tooltipHandler;
        [SerializeField]
        private TextMeshProUGUI contentText;

        //TODO: inject
        internal void Inject(ITooltipHandler tooltipHandler)
        {
            this.tooltipHandler = tooltipHandler;
        }
    }
}