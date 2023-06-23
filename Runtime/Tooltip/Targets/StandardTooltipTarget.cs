using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Tooltip.Targets
{
    using BroWar.UI.Tooltip.Behaviours;

    [AddComponentMenu("BroWar/UI/Tooltip/Targets/Tooltip Target (Default)")]
    public class StandardTooltipTarget : TooltipTarget<StandardTooltipBehaviour>
    {
        [Title("Content")]
        [SerializeField, TextArea(4, 8)]
        private string tooltipContent;

        protected override void UpdateContent(StandardTooltipBehaviour tooltip)
        {
            tooltip.UpdateContent(tooltipContent);
            var transform = tooltip.RectTransform;
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
        }

        protected override bool ShouldShowContent => base.ShouldShowContent && !string.IsNullOrEmpty(tooltipContent);
    }
}