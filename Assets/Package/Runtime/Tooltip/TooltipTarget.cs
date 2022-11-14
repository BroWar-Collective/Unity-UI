using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BroWar.UI.Tooltip
{
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Tooltip/Tooltip Target")]
    public class TooltipTarget : MonoBehaviour
    {
        private ITooltipHandler tooltipHandler;

        [Inject]
        internal void Inject(ITooltipHandler tooltipHandler)
        {
            this.tooltipHandler = tooltipHandler;
        }

        //TODO: temporary
        [Inject]
        internal void Inject(List<IUiHandler> handlers)
        { }
    }
}