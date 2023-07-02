using DG.Tweening;

namespace BroWar.UI.Animation
{
    public interface IAnimationContext
    {
        Sequence GetSequence(bool fromSequence);
        Sequence GetSequence(bool fromSequence, Sequence sequence);
    }
}