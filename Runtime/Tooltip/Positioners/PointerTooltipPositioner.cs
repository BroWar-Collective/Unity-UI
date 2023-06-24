using UnityEngine;

namespace BroWar.UI.Tooltip.Positioners
{
    public class PointerTooltipPositioner : ITooltipPositioner
    {
        [SerializeField]
        private RectTransform target;

        public Vector2 GetScreenPosition(Vector2? pointerPosition)
        {
            if (pointerPosition.HasValue)
            {
                return pointerPosition.Value;
            }

            var worldPosition = target.position;
            return new Vector2(worldPosition.x, worldPosition.y);
        }
    }
}