using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Tooltip.Targets
{
    [AddComponentMenu("BroWar/UI/Tooltip/Tooltip Target")]
    public class StandardTooltipTarget : TooltipTarget<TooltipBehaviour>
    {
        [Title("Content")]
        [SerializeField, TextArea(4, 8)]
        private string tooltipContent;

        protected override void UpdateContent(TooltipBehaviour tooltip)
        {
            tooltip.UpdateContent(tooltipContent);
            var transform = tooltip.RectTransform;
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
        }

        protected override bool ShouldShowContent => base.ShouldShowContent && !string.IsNullOrEmpty(tooltipContent);
    }
}