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
            Vector2 inPos;
            switch (direction)
            {
                case AnimationDirection.Left:
                    inPos = new Vector2(rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Right:
                    inPos = new Vector2(-rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Up:
                    inPos = new Vector2(0, -rectTransform.rect.height);
                    break;
                case AnimationDirection.Down:
                    inPos = new Vector2(0, rectTransform.rect.height);
                    break;
                default:
                    inPos = Vector2.zero;
                    break;
            }

            rectTransform.anchoredPosition = inPos;
            Tween anchorMoveTween =
                rectTransform.DOAnchorPos(Vector2.zero, duration).SetEase(ease);
            sequence.Insert(0, anchorMoveTween);

            return sequence;
        }

        public static Sequence SlideOut(RectTransform rectTransform, AnimationDirection dir,
            Ease ease = Ease.OutCubic, float duration = 1.0f)
        {
            var sequence = DOTween.Sequence();
            rectTransform.anchoredPosition = Vector2.zero;
            Vector2 outPos;
            switch (dir)
            {
                case AnimationDirection.Left:
                    outPos = new Vector2(-rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Right:
                    outPos = new Vector2(rectTransform.rect.width, 0);
                    break;
                case AnimationDirection.Up:
                    outPos = new Vector2(0, rectTransform.rect.height);
                    break;
                case AnimationDirection.Down:
                    outPos = new Vector2(0, -rectTransform.rect.height);
                    break;
                default:
                    outPos = Vector2.zero;
                    break;
            }

            Tween anchorMoveTween =
                rectTransform.DOAnchorPos(outPos, duration).SetEase(ease);
            sequence.Insert(0, anchorMoveTween);
            return sequence;
        }
    }
}