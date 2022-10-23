using DG.Tweening;
using UnityEngine;

namespace BroWar.UI
{
    public static class AnimationUtility
    {
        public static Sequence SlideIn(RectTransform rectTransform, AnimationDirection direction,
            Ease ease = Ease.OutCubic, float duration = 1.0f)
        {
            var sequence = DOTween.Sequence();
            Vector2 inPosition;
            switch (direction)
            {
                case AnimationDirection.Left:
                    inPosition = new Vector2(rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Right:
                    inPosition = new Vector2(-rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Up:
                    inPosition = new Vector2(0, -rectTransform.rect.height);
                    break;
                case AnimationDirection.Down:
                    inPosition = new Vector2(0, rectTransform.rect.height);
                    break;
                default:
                    inPosition = Vector2.zero;
                    break;
            }

            rectTransform.anchoredPosition = inPosition;
            Tween anchorMoveTween =
                rectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(ease);
            sequence.Insert(0, anchorMoveTween);

            return sequence;
        }

        public static Sequence SlideOut(RectTransform rectTransform, AnimationDirection direction,
            Ease ease = Ease.OutCubic, float duration = 1.0f)
        {
            var sequence = DOTween.Sequence();
            rectTransform.anchoredPosition = Vector2.zero;
            Vector2 outPosition;
            switch (direction)
            {
                case AnimationDirection.Left:
                    outPosition = new Vector2(-rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Right:
                    outPosition = new Vector2(rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Up:
                    outPosition = new Vector2(0, rectTransform.rect.height);
                    break;
                case AnimationDirection.Down:
                    outPosition = new Vector2(0, -rectTransform.rect.height);
                    break;
                default:
                    outPosition = Vector2.zero;
                    break;
            }

            Tween anchorMoveTween =
                rectTransform.DOAnchorPos(outPosition, duration).SetEase(ease);
            sequence.Insert(0, anchorMoveTween);
            return sequence;
        }
    }
}