using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Tooltip
{
    [AddComponentMenu("BroWar/UI/Tooltip/Tooltip Behaviour")]
    public class TooltipBehaviour : UiObject
    {
        [SerializeField, NotNull]
        private LayoutGroup contentGroup;
        [SerializeField, NotNull]
        private RectTransform contentRect;
        [SerializeField, NotNull]
        private TextMeshProUGUI contentText;
        [SerializeField]
        private TooltipData defaultData;
        private TooltipData currentData;

        public void UpdatePositionAndData(Vector2 position)
        {
            UpdatePositionAndData(position, in defaultData);
        }

        public void UpdatePositionAndData(Vector2 position, in TooltipData data)
        {
            currentData = data;
            contentGroup.childAlignment = currentData.childAlignment;
            UpdatePosition(position);
        }

        public virtual void UpdateContent(string text)
        {
            contentText.SetText(text);
        }

        public virtual void UpdatePosition(Vector2 screenPosition)
        {
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