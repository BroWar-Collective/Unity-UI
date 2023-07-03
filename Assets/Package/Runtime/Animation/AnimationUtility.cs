using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Animation
{
    /// <summary>
    /// Utility class used to store commonly used <see cref="Tween"/>s.
    /// </summary>
    public static class AnimationUtility
    {
        public static Tween CreateSlideInTween(RectTransform rectTransform, AnimationDirection direction, Ease ease = Ease.OutCubic, float duration = 1.0f, bool fromSequence = true)
        {
            Vector2 startPosition;
            switch (direction)
            {
                case AnimationDirection.Left:
                    startPosition = new Vector2(rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Right:
                    startPosition = new Vector2(-rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Up:
                    startPosition = new Vector2(0, -rectTransform.rect.height);
                    break;
                case AnimationDirection.Down:
                    startPosition = new Vector2(0, rectTransform.rect.height);
                    break;
                default:
                    startPosition = Vector2.zero;
                    break;
            }

            var anchorMoveTween =
                rectTransform.DOAnchorPos(Vector2.zero, duration)
                .SetEase(ease);
            if (fromSequence)
            {
                anchorMoveTween.From(startPosition);
            }

            return anchorMoveTween;
        }

        public static Tween CreateSlideOutTween(RectTransform rectTransform, AnimationDirection direction, Ease ease = Ease.OutCubic, float duration = 1.0f, bool fromSequence = true)
        {
            Vector2 targetPosition;
            switch (direction)
            {
                case AnimationDirection.Left:
                    targetPosition = new Vector2(-rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Right:
                    targetPosition = new Vector2(rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Up:
                    targetPosition = new Vector2(0, rectTransform.rect.height);
                    break;
                case AnimationDirection.Down:
                    targetPosition = new Vector2(0, -rectTransform.rect.height);
                    break;
                default:
                    targetPosition = Vector2.zero;
                    break;
            }

            var anchorMoveTween =
                rectTransform.DOAnchorPos(targetPosition, duration)
                .SetEase(ease);
            if (fromSequence)
            {
                anchorMoveTween.From(Vector2.zero);
            }

            return anchorMoveTween;
        }
    }
}