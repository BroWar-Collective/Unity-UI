using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Common
{
    using BroWar.UI.Animation;

    //TODO: rename
    [Serializable]
    public class AnimationShowHideHandler : IActivityHandler
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
            return showAnimationContext?.GetSequence();
        }

        private Sequence GetHideSequence()
        {
            return hideAnimationContext?.GetSequence();
        }

        public void Show(IHidableObject target)
        {
            Show(target, false);
        }

        public void Hide(IHidableObject target)
        {
            Hide(target, false);
        }

        public void Show(IHidableObject target, bool immediately, Action onFinish = null)
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

        public void Hide(IHidableObject target, bool immediately, Action onFinish = null)
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