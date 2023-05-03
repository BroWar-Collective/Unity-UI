﻿using DG.Tweening;
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
            if (sequence != null)
            {
                sequence.Kill();
            }
        }

        protected virtual Sequence GetShowSequence()
        {
            return showAnimationContext?.GetSequence(RectTransform);
        }

        protected virtual Sequence GetHideSequence()
        {
            return hideAnimationContext?.GetSequence(RectTransform);
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

        /// <summary>
        /// Indicates if <see cref="UiPanel"/> should use animations when hiding or showing.
        /// </summary>
        public virtual bool UseAnimations { get; private set; } = true;
        public virtual IAnimationContext ShowAnimationContext { get => showAnimationContext; set => showAnimationContext = value; }
        public virtual IAnimationContext HideAnimationContext { get => hideAnimationContext; set => hideAnimationContext = value; }
    }
}