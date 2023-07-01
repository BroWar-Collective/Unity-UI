using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace BroWar.UI.Animation
{
    [Serializable, MovedFrom(false, null, null, "MultipleAnimationsContext")]
    public class GroupAnimationsContext : IAnimationContext
    {
        [SerializeField, SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        [ReorderableList(Foldable = true)]
        private IAnimationContext[] nestedContexts;
        [SerializeField]
        private bool parallel = true;

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

                if (parallel)
                {
                    var nestedSequence = context.GetSequence();
                    sequence.Join(nestedSequence);
                }
                else
                {
                    context.GetSequence(sequence);
                }
            }

            return sequence;
        }
    }
}