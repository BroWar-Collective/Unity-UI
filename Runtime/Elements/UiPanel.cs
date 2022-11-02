using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Elements
{
    using BroWar.UI.Animation;

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
            if (sequence != null)
            {
                sequence.Kill();
            }
        }

        protected virtual Sequence GetShowSequence()
        {
            return showAnimationContext?.GetSequence(rectTransform);
        }

        protected virtual Sequence GetHideSequence()
        {
            return hideAnimationContext?.GetSequence(rectTransform);
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
                    sequence.Play();
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
                    sequence.Play();
                    return;
                }
            }

            base.Hide();
        }

        public virtual bool UseAnimations { get; private set; } = true;
        public virtual IAnimationContext ShowAnimationContext { get => showAnimationContext; set => showAnimationContext = value; }
        public virtual IAnimationContext HideAnimationContext { get => hideAnimationContext; set => hideAnimationContext = value; }
    }
}