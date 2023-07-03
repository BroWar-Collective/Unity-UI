using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace BroWar.UI.Animation
{
    [Serializable, MovedFrom(false, null, null, "MultipleAnimationsContext")]
    public class GroupAnimationsContext : IAnimationContext
    {
        [SerializeField]
        private bool parallel = true;
        [SerializeField, SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        [ReorderableList(Foldable = true)]
        private IAnimationContext[] nestedContexts;

        public Sequence GetSequence(bool fromSequence)
        {
            var sequence = DOTween.Sequence();
            return GetSequence(fromSequence, sequence);
        }

        public Sequence GetSequence(bool fromSequence, Sequence sequence)
        {
            foreach (var context in nestedContexts)
            {
                if (context == null)
                {
                    continue;
                }

                if (parallel)
                {
                    var nestedSequence = context.GetSequence(fromSequence);
                    sequence.Join(nestedSequence);
                }
                else
                {
                    context.GetSequence(fromSequence, sequence);
                }
            }

            return sequence;
        }
    }
}