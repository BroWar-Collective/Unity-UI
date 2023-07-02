using DG.Tweening;

namespace BroWar.UI.Animation
{
    public interface IAnimationContext
    {
        Sequence GetSequence();
        Sequence GetSequence(Sequence sequence); 
        Tween CreateAnimationTween();
    }
}