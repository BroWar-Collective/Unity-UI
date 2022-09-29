using BroWar.UI;
using DG.Tweening;

namespace Examples
{
    public class SourceBarView : UiView
    {
        public override Sequence GetTransitionInSequence()
        {
            return AnimationUtility.SlideIn(rectTransform, AnimationDirection.Down);
        }

        public override Sequence GetTransitionOutSequence()
        {
            return AnimationUtility.SlideOut(rectTransform, AnimationDirection.Up);
        }
    }
}