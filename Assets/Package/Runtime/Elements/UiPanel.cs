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

        //TODO: internal show in time
        public override void Show()
        {
            base.Show();
            if (UseAnimations)
            {
                ResetAnimation();
                sequence = GetShowSequence();
                sequence.Play();
            }
        }

        public override void Hide()
        {
            if (UseAnimations)
            {
                ResetAnimation();
                sequence = GetHideSequence();
                sequence.AppendCallback(base.Hide);
                sequence.Play();
                return;
            }

            base.Hide();
        }

        public virtual Sequence GetShowSequence()
        {
            return AnimationUtility.SlideIn(rectTransform, AnimationDirection.Left);
        }

        public virtual Sequence GetHideSequence()
        {
            return AnimationUtility.SlideOut(rectTransform, AnimationDirection.Right);
        }

        public virtual bool UseAnimations { get; private set; } = true;
    }
}