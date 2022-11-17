using TMPro;
using UnityEngine;

namespace BroWar.UI.Tooltip
{
    [AddComponentMenu("BroWar/UI/Tooltip/Tooltip Behaviour")]
    public class TooltipBehaviour : UiObject
    {
        [SerializeField, NotNull]
        private TextMeshProUGUI contentText;
        [SerializeField]
        private Vector2 positionOffset;

        public void UpdateText(string text)
        {
            contentText.SetText(text);
        }

        public void UpdateRect(Vector2 position)
        {
            position += positionOffset;
            var pivotX = position.x / Screen.width;
            var pivotY = position.y / Screen.height;
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            rectTransform.position = position;
        }
    }
}