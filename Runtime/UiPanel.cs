using DG.Tweening;
using UnityEngine;

namespace BroWar.UI
{
    public class UiPanel : UiObject
    {
        protected RectTransform rectTransform;

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public override void Show()
        {
            base.Show();
            //TODO: temporary solution
            Sequence inSequence = GetTransitionInSequence();
            inSequence.Play();
        }

        public override void Hide()
        {
            base.Hide();
            //TODO: animation
        }

        public virtual Sequence GetTransitionInSequence()
        {
            return AnimationUtility.SlideIn(rectTransform, AnimationDirection.Left);
        }

        public virtual Sequence GetTransitionOutSequence()
        {
            return AnimationUtility.SlideOut(rectTransform, AnimationDirection.Right);
        }
    }
}