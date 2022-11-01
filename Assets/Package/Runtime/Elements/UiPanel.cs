using DG.Tweening;

namespace BroWar.UI.Elements
{
    using BroWar.UI.Animation;

    public class UiPanel : UiObject
    {
        //TODO: serializable animation data
        private Sequence sequence;

        private void ResetAnimation()
        {
            if (sequence != null)
            {
                sequence.Kill();
            }
        }

        public override void Show()
        {
            base.Show();
            if (UseAnimations)
            {
                ResetAnimation();
                sequence = GetTransitionInSequence();
                sequence.Play();
            }
        }

        public override void Hide()
        {
            if (UseAnimations)
            {
                ResetAnimation();
                sequence = GetTransitionOutSequence();
                sequence.AppendCallback(base.Hide);
                sequence.Play();
                return;
            }

            base.Hide();
        }

        public virtual Sequence GetTransitionInSequence()
        {
            return AnimationUtility.SlideIn(rectTransform, AnimationDirection.Left);
        }

        public virtual Sequence GetTransitionOutSequence()
        {
            return AnimationUtility.SlideOut(rectTransform, AnimationDirection.Right);
        }

        public virtual bool UseAnimations { get; private set; } = true;
    }
}