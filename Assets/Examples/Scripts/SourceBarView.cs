using BroWar.UI.Animation;
using BroWar.UI.Elements;
using DG.Tweening;

namespace Examples
{
    public class SourceBarView : UiView
    {
        public override Sequence GetShowSequence()
        {
            return AnimationUtility.SlideIn(rectTransform, AnimationDirection.Down, duration: 2.0f);
        }

        public override Sequence GetHideSequence()
        {
            return AnimationUtility.SlideOut(rectTransform, AnimationDirection.Up, duration: 2.0f);
        }
    }
}