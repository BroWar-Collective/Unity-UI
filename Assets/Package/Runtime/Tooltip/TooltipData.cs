using System;
using UnityEngine;

namespace BroWar.UI.Tooltip
{
    //TODO: replace it with SO-based settings (?)
    [Serializable]
    public class TooltipData
    {
        public Vector2 positionOffset;
        public Vector2 positionPivot;
        public TextAnchor childAlignment;
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        public ITooltipPositioner positioner;
    }
}