﻿using TMPro;
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

        public virtual void UpdatePosition(Vector2 position)
        {
            var rect = RectTransform.rect;
            var w = Screen.width;
            var h = Screen.height;
            var pivot = currentData.positionPivot;
            position += currentData.positionOffset;
            position.x = Mathf.Clamp(position.x, rect.width * pivot.x, w - rect.width * (1.0f - pivot.x));
            position.y = Mathf.Clamp(position.y, rect.height * pivot.y, h - rect.height * (1.0f - pivot.y));
            RectTransform.pivot = pivot;
            RectTransform.position = position;
        }
    }
}