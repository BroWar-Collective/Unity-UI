using UnityEngine;

namespace BroWar.UI.Utilities
{
    public static class RectTransformUtility
    {
        public static void CopyRectTransform(RectTransform target, RectTransform reference)
        {
            target.anchorMin = reference.anchorMin;
            target.anchorMax = reference.anchorMax;
            target.anchoredPosition = reference.anchoredPosition;
            target.sizeDelta = reference.sizeDelta;
            target.localRotation = reference.localRotation;
            target.localScale = reference.localScale;
        }
    }
}
