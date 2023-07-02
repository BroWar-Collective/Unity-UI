using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Animation.Animations
{
    using BroWar.Common;

    [Serializable]
    public class FadeInAnimationContext : IAnimationContext
    {
        [SerializeField]
        private CanvasGroup group;
        [SerializeField, Min(0)]
        private float duration = 1.0f;

        public Sequence GetSequence()
        {
            var sequence = DOTween.Sequence();
            return GetSequence(sequence);
        }

        public Sequence GetSequence(Sequence sequence)
        {
            if (group == null)
            {
                LogHandler.Log($"[UI][Animation] {nameof(CanvasGroup)} not available.", LogType.Warning);
                return sequence;
            }

            //DOTween.To(
            //    () => group.alpha, 
            //    (value) => 
            //    {
            //        group.alpha = value;
            //        Debug.LogError("SET VALUE - " + value);
            //    }, 1.0f, duration)
            //    //.Pause()
            //    .SetEase(Ease.InCubic)
            //    .SetAutoKill(false);
            //sequence.Append(group.DOFade(1.0f, duration).OnUpdate(() => Debug.LogError("ASDADS")));
            return sequence;
        }

        public Tween CreateAnimationTween()
        {
            return DOTween.To(
                () => group.alpha,
                (value) =>
                {
                    group.alpha = value;
                    Debug.LogError("SET VALUE - " + value);
                }, 1.0f, duration)
                .From(0)
                .SetEase(Ease.InCubic);
        }
    }
}