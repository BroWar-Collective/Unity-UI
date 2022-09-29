using DG.Tweening;
using UnityEngine;

namespace BroWar.UI
{
    public class UiView : UiObject
    {
        protected RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public override void Show()
        {
            base.Show();
            //TODO: temporary solution, we should run animation from the manager
            Sequence inSequence = GetTransitionInSequence();
            inSequence.Play();
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