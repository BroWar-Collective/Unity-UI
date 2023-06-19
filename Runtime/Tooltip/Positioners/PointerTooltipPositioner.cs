using UnityEngine;

namespace BroWar.UI.Tooltip.Positioners
{
    public class PointerTooltipPositioner : ITooltipPositioner
    {
        [SerializeField]
        private RectTransform target;

        public Vector2 GetScreenPosition(Vector2? pointerPosition)
        {
            return pointerPosition ?? Vector2.zero;
        }
    }
}