using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Elements
{
    using BroWar.UI.Animation;

    //TODO: refactor

    [AddComponentMenu("BroWar/UI/Elements/UI Panel")]
    public class UiPanel : UiObject
    {
        //NOTE: multiple animations?
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
            base.Show();
            if (UseAnimations)
            {
                ResetAnimation();
                sequence = GetShowSequence();
                if (sequence != null)
                {
                    StartShowAnimation();
                }
            }
        }

        public override void Hide()
        {
            if (UseAnimations)
            {
                ResetAnimation();
                sequence = GetHideSequence();
                if (sequence != null)
                {
                    sequence.AppendCallback(base.Hide);
                    StartHideAnimation();
                    return;
                }
            }

            base.Hide();
        }

        //TODO: better name
        //TODO: separate into is hiding is showing
        public bool IsDuringAnimation { get; private set; }
        public bool Shows { get; private set; }
        public bool Hides { get; private set; }

        /// <summary>
        /// Indicates if <see cref="UiPanel"/> should use animations when hiding or showing.
        /// </summary>
        public virtual bool UseAnimations { get; private set; } = true;
        public virtual IAnimationContext ShowAnimationContext { get => showAnimationContext; set => showAnimationContext = value; }
        public virtual IAnimationContext HideAnimationContext { get => hideAnimationContext; set => hideAnimationContext = value; }
    }
}