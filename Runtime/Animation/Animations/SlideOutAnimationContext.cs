using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Animation.Animations
{
    using BroWar.Common;

    [Serializable]
    public class SlideOutAnimationContext : IAnimationContext
    {
        [SerializeField]
        private RectTransform transform;
        [SerializeField]
        private AnimationDirection direction;
        [SerializeField, SearchableEnum]
        private Ease ease = Ease.OutCubic;
        [SerializeField, Min(0)]
        private float duration = 1.0f;

        public Sequence GetSequence(bool fromSequence)
        {
            var sequence = DOTween.Sequence();
            return GetSequence(fromSequence, sequence);
        }

        public Sequence GetSequence(bool fromSequence, Sequence sequence)
        {
            if (transform == null)
            {
                LogHandler.Log($"[UI][Animation] {nameof(RectTransform)} not available.", LogType.Warning);
                return sequence;
            }

            sequence.Append(CreateAnimationTween(fromSequence));
            return sequence;
        }

        public Tween CreateAnimationTween(bool fromSequence)
        {
            return AnimationUtility.CreateSlideOutTween(transform, direction, ease, duration, fromSequence);
        }
    }
}