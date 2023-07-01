using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Animation.Animations
{
    using BroWar.Common;

    [Serializable]
    public class SlideInAnimationContext : IAnimationContext
    {
        [SerializeField]
        private RectTransform transform;
        [SerializeField]
        private AnimationDirection direction;
        [SerializeField, SearchableEnum]
        private Ease ease = Ease.OutCubic;
        [SerializeField, Min(0)]
        private float duration = 1.0f;

        public Sequence GetSequence()
        {
            var sequence = DOTween.Sequence();
            return GetSequence(sequence);
        }

        public Sequence GetSequence(Sequence sequence)
        {
            if (transform == null)
            {
                LogHandler.Log($"[UI][Animation] {nameof(RectTransform)} not available.", LogType.Warning);
                return sequence;
            }

            return AnimationUtility.SlideIn(sequence, transform, direction, ease, duration);
        }
    }
}