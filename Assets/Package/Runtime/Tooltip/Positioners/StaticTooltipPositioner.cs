using System;
using UnityEngine;

namespace BroWar.UI.Tooltip.Positioners
{
    [Serializable]
    public class StaticTooltipPositioner : ITooltipPositioner
    {
        [SerializeField]
        private RectTransform target;

        public Vector2 GetScreenPosition(Vector2? pointerPosition)
        {
            var worldPosition = target.position;
            return new Vector2(worldPosition.x, worldPosition.y);
        }
    }
}