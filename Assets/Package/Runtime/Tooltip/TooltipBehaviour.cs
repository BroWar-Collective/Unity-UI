using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Tooltip
{
    [AddComponentMenu("BroWar/UI/Tooltip/Tooltip Behaviour")]
    public class TooltipBehaviour : UiObject
    {
        [SerializeField, NotNull]
        private RectTransform contentRect;
        [SerializeField, NotNull]
        private TextMeshProUGUI contentText;
        [SerializeField]
        private float maxWidth = 250.0f;
        [SerializeField]
        private TooltipSettings defaultSettings;
        private TooltipSettings currentSettings;

        //TODO: temporary solution
        private void FixRectSize()
        {
            var parentSize = rectTransform.sizeDelta;
            parentSize.x = maxWidth;
            rectTransform.sizeDelta = parentSize;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            if (contentRect.sizeDelta.x < maxWidth)
            {
                parentSize.x = contentRect.sizeDelta.x;
            }
            else
            {
                parentSize.x = maxWidth;
            }

            rectTransform.sizeDelta = parentSize;
        }

        public void UpdateContent(string text)
        {
            UpdateContent(text, in defaultSettings);
        }

        public void UpdateContent(string text, in TooltipSettings settings)
        {
            contentText.SetText(text);
            currentSettings = settings;
            FixRectSize();
        }

        public void UpdatePosition(Vector2 position)
        {
            //TODO: test different cases and pivots
            var rect = contentRect.rect;
            var w = Screen.width;
            var h = Screen.height;
            var pivot = currentSettings.positionPivot;
            position += currentSettings.positionOffset;
            position.x = Mathf.Clamp(position.x, rect.width * pivot.x, w - rect.width * (1.0f - pivot.x));
            position.y = Mathf.Clamp(position.y, rect.height * pivot.y, h - rect.height * (1.0f - pivot.y));
            rectTransform.pivot = currentSettings.positionPivot;
            rectTransform.position = position;
        }
    }
}