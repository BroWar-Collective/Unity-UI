using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace BroWar.UI.Animation.Animations
{
    [Serializable, MovedFrom(false, "BroWar.UI.Animation", null, "GroupAnimationsContext")]
    public class GroupedAnimationsContext : IAnimationContext
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