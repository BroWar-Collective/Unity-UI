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

        public Tween CreateAnimationTween()
        {
            Vector2 inPosition;
            switch (direction)
            {
                case AnimationDirection.Left:
                    inPosition = new Vector2(transform.rect.width, 0);
                    break;
                case AnimationDirection.Right:
                    inPosition = new Vector2(-transform.rect.width, 0);
                    break;
                case AnimationDirection.Up:
                    inPosition = new Vector2(0, -transform.rect.height);
                    break;
                case AnimationDirection.Down:
                    inPosition = new Vector2(0, transform.rect.height);
                    break;
                default:
                    inPosition = Vector2.zero;
                    break;
            }

            //if (fixPosition)
            //{
  

            //    rectTransform.anchoredPosition = inPosition;
            //}

            Tween anchorMoveTween =
                transform.DOAnchorPos(Vector2.zero, duration)
                .SetEase(ease)
                .From(inPosition);
            return anchorMoveTween;
        }

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