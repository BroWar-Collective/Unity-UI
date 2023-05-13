using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Elements
{
    using BroWar.UI.Animation;

    [AddComponentMenu("BroWar/UI/Elements/UI Panel")]
    public class UiPanel : UiObject
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

        protected virtual Sequence GetShowSequence()
        {
            return showAnimationContext?.GetSequence();
        }

        protected virtual Sequence GetHideSequence()
        {
            return hideAnimationContext?.GetSequence();
        }

        public override void Show()
        {
            Show(false);
        }

        public override void Hide()
        {
            Hide(false);
        }

        public virtual void Show(bool immediately, Action onFinish = null)
        {
            base.Show();
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

        public virtual void Hide(bool immediately, Action onFinish = null)
        {
            ResetAnimation();
            sequence = GetHideSequence();
            if (sequence == null)
            {
                base.Hide();
                onFinish?.Invoke();
                return;
            }

            sequence.AppendCallback(() =>
            {
                base.Hide();
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
        public virtual IAnimationContext ShowAnimationContext { get => showAnimationContext; set => showAnimationContext = value; }
        public virtual IAnimationContext HideAnimationContext { get => hideAnimationContext; set => hideAnimationContext = value; }
    }
}