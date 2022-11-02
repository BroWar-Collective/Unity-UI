using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Animation.Animations
{
    [Serializable]
    public class SlideInAnimationContext : IAnimationContext
    {
        [SerializeField]
        private AnimationDirection direction;
        [SerializeField, SearchableEnum]
        private Ease ease = Ease.OutCubic;
        [SerializeField, Min(0)]
        private float duration = 1.0f;

        public Sequence GetSequence(RectTransform rectTransform)
        {
            return AnimationUtility.SlideIn(rectTransform, direction, ease, duration);
        }
    }
}