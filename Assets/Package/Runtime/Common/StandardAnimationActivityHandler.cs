using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace BroWar.UI.Common
{
    using BroWar.UI.Animation;

    /// <summary>
    /// Animation-based activity handler.
    /// Cached animation is created only once and then re-used. 
    /// Only one, shared sequence is used to hide and show <see cref="IActivityTarget"/>.
    /// </summary>
    [Serializable]
    [MovedFrom(false, null, null, "SimpleAnimationActivityHandler")]
    public class StandardAnimationActivityHandler : IActivityHandler
    {
        [Title("Animations")]
        [SerializeField, SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        [NewLabel("-> Animation Context"), FormerlySerializedAs("showAnimationContext")]
        private IAnimationContext animationContext;

        private Sequence sequence;

        private void OnAnimationFinish()
        {
            Shows = false;
            Hides = false;
        }

        private void StartShowAnimation(Sequence sequence)
        {
            if (sequence == null)
            {
                return;
            }

            sequence.PlayForward();
            Shows = true;
            Hides = false;
        }

        private void StartHideAnimation(Sequence sequence)
        {
            if (sequence == null)
            {
                return;
            }

            sequence.PlayBackwards();
            Hides = true;
            Shows = false;
        }

        private Sequence GetAnimationSequence()
        {
            if (sequence == null)
            {
                sequence = GetReusableAnimation(animationContext);
            }

            return sequence;
        }

        private Sequence GetReusableAnimation(IAnimationContext animationContext)
        {
            var sequence = animationContext?.GetSequence(true);
            sequence.Pause();
            sequence.SetAutoKill(false);
            sequence.Complete();
            return sequence;
        }

        public void Show(IActivityTarget target)
        {
            Show(target, false);
        }

        public void Hide(IActivityTarget target)
        {
            Hide(target, false);
        }

        public void Show(IActivityTarget target, bool immediately, Action onFinish = null)
        {
            target.Show();

            var sequence = GetAnimationSequence();
            if (sequence == null || sequence.IsComplete())
            {
                onFinish?.Invoke();
                return;
            }

            sequence.OnComplete(() =>
            {
                OnAnimationFinish();
                onFinish?.Invoke();
            });

            StartShowAnimation(sequence);
            if (immediately)
            {
                sequence.Complete(true);
            }
        }

        public void Hide(IActivityTarget target, bool immediately, Action onFinish = null)
        {
            var sequence = GetAnimationSequence();
            if (sequence == null)
            {
                target.Hide();
                onFinish?.Invoke();
                return;
            }

            sequence.OnRewind(() =>
            {
                OnAnimationFinish();
                target.Hide();
                onFinish?.Invoke();
            });

            StartHideAnimation(sequence);
            if (immediately)
            {
                sequence.GotoWithCallbacks(0);
            }
        }

        public void Dispose()
        {
            sequence?.Kill(true);
            sequence = null;
        }

        public bool Shows { get; private set; }
        public bool Hides { get; private set; }
        public IAnimationContext AnimationContext => animationContext;
    }
}