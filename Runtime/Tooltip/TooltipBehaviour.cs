using System;
using UnityEngine;

namespace BroWar.UI.Tooltip
{
    /// <summary>
    /// Basic in-game tooltip representation.
    /// </summary>
    [AddComponentMenu("BroWar/UI/Tooltip/Behaviours/Tooltip Behaviour (Basic)")]
    public class TooltipBehaviour : UiObject, IDisposable
    {
        [Title("Data")]
        [SerializeField]
        private TooltipData defaultData;
        private TooltipData currentData;

        [Title("Content")]
        [SerializeField, NotNull]
        private RectTransform contentRect;

        private Vector2 GetScreenPosition(Vector2? pointerPosition)
        {
            var positioner = currentData.positioner;
            if (positioner != null)
            {
                return positioner.GetScreenPosition(pointerPosition);
            }
            else
            {
                return pointerPosition ?? Vector2.zero;
            }
        }

        protected virtual void OnDataUpdate(TooltipData data)
        { }

        public void Prepare()
        {
            Prepare(defaultData);
        }

        public virtual void Prepare(TooltipData data)
        {
            currentData = data;
            OnDataUpdate(data);
        }

        public virtual void Dispose()
        {
            currentData = null;
        }

        public virtual void UpdatePosition(Vector2? pointerPosition)
        {
            var screenPosition = GetScreenPosition(pointerPosition);

            var rect = RectTransform.rect;
            var w = Screen.width;
            var h = Screen.height;
            var pivot = currentData.positionPivot;
            screenPosition += currentData.positionOffset;
            screenPosition.x = Mathf.Clamp(screenPosition.x, rect.width * pivot.x, w - rect.width * (1.0f - pivot.x));
            screenPosition.y = Mathf.Clamp(screenPosition.y, rect.height * pivot.y, h - rect.height * (1.0f - pivot.y));
            RectTransform.pivot = pivot;
            RectTransform.position = screenPosition;
        }

        /// <summary>
        /// Instance ID of associated pool. 
        /// Used internally by the tooltip system.
        /// </summary>
        internal int InstanceId { get; set; }
    }
}