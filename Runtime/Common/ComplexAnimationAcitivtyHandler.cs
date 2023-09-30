using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Common
{
    using BroWar.UI.Animation;

    /// <summary>
    /// Animation-based activity handler.
    /// Creates new sequences every time new operation is requested.
    /// Uses separate animations for hiding and showing <see cref="IActivityTarget"/>s.
    /// </summary>
    [Serializable]
    public class ComplexAnimationAcitivtyHandler : IActivityHandler
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
            sequence = null;
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
            return showAnimationContext?.GetSequence(false);
        }

        private Sequence GetHideSequence()
        {
            return hideAnimationContext?.GetSequence(false);
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
            target.SetActive(true);
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
            var callback = new TweenCallback(() =>
            {
                target.SetActive(false);
                onFinish?.Invoke();
            });

            if (sequence == null)
            {
                callback();
                return;
            }

            sequence.AppendCallback(callback);
            if (immediately)
            {
                sequence.Complete(true);
            }
            else
            {
                StartHideAnimation();
            }
        }

        public void Dispose()
        {
            ResetAnimation();
        }

        public bool Shows { get; private set; }
        public bool Hides { get; private set; }
        public IAnimationContext ShowAnimationContext => showAnimationContext;
        public IAnimationContext HideAnimationContext => hideAnimationContext;
    }
}