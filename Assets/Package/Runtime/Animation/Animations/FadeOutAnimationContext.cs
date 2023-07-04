using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Animation.Animations
{
    using BroWar.Common;

    [Serializable]
    public class FadeOutAnimationContext : IAnimationContext
    {
        [SerializeField]
        private CanvasGroup group;
        [SerializeField, Min(0)]
        private float duration = 1.0f;

        public Sequence GetSequence(bool fromSequence)
        {
            var sequence = DOTween.Sequence();
            return GetSequence(fromSequence, sequence);
        }

        public Sequence GetSequence(bool fromSequence, Sequence sequence)
        {
            if (group == null)
            {
                LogHandler.Log($"[UI][Animation] {nameof(CanvasGroup)} not available.", LogType.Warning);
                return sequence;
            }

            sequence.Append(CreateAnimationTween(fromSequence));
            return sequence;
        }

        public Tween CreateAnimationTween(bool fromSequence)
        {
            var tween = group.DOFade(0, duration);
            if (fromSequence)
            {
                tween.From(1);
            }

            return tween;
        }
    }
}