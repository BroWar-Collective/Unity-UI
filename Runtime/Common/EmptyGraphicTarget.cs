using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Common
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("BroWar/UI/Common/Empty Graphic Target")]
    public class EmptyGraphicTarget : Graphic
    {
        protected override void UpdateGeometry()
        { }
    }
}