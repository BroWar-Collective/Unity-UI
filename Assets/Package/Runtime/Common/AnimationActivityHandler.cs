using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace BroWar.UI.Common
{
    using BroWar.UI.Animation;

    /// <summary>
    /// Animation-based activity handler.
    /// </summary>
    [Serializable]
    [MovedFrom(false, null, null, "AnimationShowHideHandler")]
    public class AnimationActivityHandler : IActivityHandler
    {
        [Title("Animations")]
        [SerializeField, SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        [NewLabel("-> Animation Context")]
        private IAnimationContext showAnimationContext;
        [SerializeField, SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        [NewLabel("<- Animation Context")]
        private IAnimationContext hideAnimationContext;

        private Sequence sequence;

        private void ResetAnimation()
        {
            sequence?.Kill();
            Shows = false;
            Hides = false;
        }

        private void StartShowAnimation()
        {
            if (sequence == null)
            {
                return;
            }

            sequence.OnComplete(() => Shows = false);
            sequence.Play();
            Shows = true;
        }

        private void StartHideAnimation()
        {
            if (sequence == null)
            {
                return;
            }

            sequence.OnComplete(() => Hides = false);
            sequence.Play();
            Hides = true;
        }

        private Sequence GetShowSequence()
        {
            UnityEngine.Debug.LogError(showAnimationContext);
            return showAnimationContext?.GetSequence();
        }

        private Sequence GetHideSequence()
        {
            UnityEngine.Debug.LogError(hideAnimationContext);
            return hideAnimationContext?.GetSequence();
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
            ResetAnimation();
            sequence = GetShowSequence();
            if (sequence == null)
            {
                onFinish?.Invoke();
                return;
            }

            sequence.AppendCallback(() =>
            {
                onFinish?.Invoke();
            });

            if (immediately)
            {
                sequence.Complete(true);
            }
            else
            {
                StartShowAnimation();
            }
        }

        public void Hide(IActivityTarget target, bool immediately, Action onFinish = null)
        {
            ResetAnimation();
            sequence = GetHideSequence();
            if (sequence == null)
            {
                target.Hide();
                onFinish?.Invoke();
                return;
            }

            sequence.AppendCallback(() =>
            {
                target.Hide();
                onFinish?.Invoke();
            });

            if (immediately)
            {
                sequence.Complete(true);
            }
            else
            {
                StartHideAnimation();
            }
        }

        public bool Shows { get; private set; }
        public bool Hides { get; private set; }
        public IAnimationContext ShowAnimationContext => showAnimationContext;
        public IAnimationContext HideAnimationContext => hideAnimationContext;
    }
}