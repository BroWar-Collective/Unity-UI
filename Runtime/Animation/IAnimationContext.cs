using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Animation
{
    public interface IAnimationContext
    {
        Sequence GetSequence(RectTransform rectTransform);
    }
}