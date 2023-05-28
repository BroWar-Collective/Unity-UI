using System;
using UnityEngine;

namespace BroWar.UI.Tooltip
{
    [Serializable]
    public struct TooltipData
    {
        public Vector2 positionOffset;
        public Vector2 positionPivot;
        public TextAnchor childAlignment;
        public TooltipPositioningType positioningType;
    }
}