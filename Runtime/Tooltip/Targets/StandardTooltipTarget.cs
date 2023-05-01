﻿using UnityEngine;

namespace BroWar.UI.Tooltip.Targets
{
    [AddComponentMenu("BroWar/UI/Tooltip/Tooltip Target")]
    public class StandardTooltipTarget : TooltipTarget<TooltipBehaviour>
    {
        [SerializeField, TextArea(4, 8)]
        private string tooltipContent;

        protected override void UpdateContent(TooltipBehaviour tooltip)
        {
            tooltip.UpdateContent(tooltipContent);
        }

        protected override bool ShouldShowContent => base.ShouldShowContent && !string.IsNullOrEmpty(tooltipContent);
    }
}