using BroWar.UI;
using DG.Tweening;

namespace Examples
{
    public class SourceBarView : UiView
    {
        public override Sequence GetTransitionInSequence()
        {
            return AnimationUtility.SlideIn(rectTransform, AnimationDirection.Down, duration: 2.0f);
        }

        public override Sequence GetTransitionOutSequence()
        {
            return AnimationUtility.SlideOut(rectTransform, AnimationDirection.Up, duration: 2.0f);
        }
    }
}