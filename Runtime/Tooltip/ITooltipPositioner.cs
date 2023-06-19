using UnityEngine;

namespace BroWar.UI.Tooltip
{
    public interface ITooltipPositioner
    {
        Vector2 GetScreenPosition(Vector2? pointerPosition);
    }
}