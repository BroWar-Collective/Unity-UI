using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Animation
{
    [Serializable]
    public class MultipleAnimationsContext : IAnimationContext
    {
        [SerializeField, SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        [ReorderableList]
        private IAnimationContext[] nestedContexts;

        public Sequence GetSequence()
        {
            var sequence = DOTween.Sequence();
            return GetSequence(sequence);
        }

        public Sequence GetSequence(Sequence sequence)
        {
            foreach (var context in nestedContexts)
            {
                if (context == null)
                {
                    continue;
                }

                context.GetSequence(sequence);
            }

            return sequence;
        }
    }
}