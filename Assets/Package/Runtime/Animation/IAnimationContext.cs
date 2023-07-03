using DG.Tweening;

namespace BroWar.UI.Animation
{
    public interface IAnimationContext
    {
        /// <summary>
        /// Creates animation <see cref="Sequence"/>.
        /// </summary>
        /// <param name="fromSequence">Indicates if <see cref="Sequence"/> should define 'From' value.</param>
        Sequence GetSequence(bool fromSequence);
        /// <summary>
        /// Appends animation <see cref="Sequence"/>.
        /// </summary>
        /// <param name="fromSequence">Indicates if <see cref="Sequence"/> should define 'From' value.</param>
        Sequence GetSequence(bool fromSequence, Sequence sequence);
    }
}