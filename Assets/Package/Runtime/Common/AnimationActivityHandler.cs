using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace BroWar.UI.Common
{
    using BroWar.UI.Animation;

    //TODO: possibility to define custom hide animation

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
        private Tween animationTween;

        private void BeginShowAnimation()
        {
            Shows = true;
            Debug.LogError("BEGIN SHOW");
        }

        private void BeginHideAnimation()
        {
            Hides = true;
            Debug.LogError("BEGIN HIDE");
        }

        private void CloseAnimation()
        {
            Shows = false;
            Hides = false;
            Debug.LogError("CLOSE");
        }

        private void ResetAnimation()
        {
            sequence?.Kill();
            Shows = false;
            Hides = false;
        }

        private void StartShowAnimation()
        {
            if (animationTween == null)
            {
                return;
            }

            //sequence.OnComplete(() => Shows = false);
            //animationTween.Restart();
            animationTween.PlayForward();
            Debug.LogError("PLAY FORWARD");
            Shows = true;
            Hides = false;
        }

        private void StartHideAnimation()
        {
            if (animationTween == null)
            {
                return;
            }

            //sequence.OnComplete(() => Hides = false);
            //animationTween.SmoothRewind();
            animationTween.PlayBackwards();
            Debug.LogError("REWIND");
            Hides = true;
            Shows = false;
        }

        private Sequence GetShowSequence()
        {
            return showAnimationContext?.GetSequence();
        }

        private Sequence GetHideSequence()
        {
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

            var tween = AnimationSequence;
            if (tween == null || tween.IsComplete())
            {
                onFinish?.Invoke();
                return;
            }

            tween.OnComplete(() =>
            {
                Debug.LogError("LOCAL ON COMPLETE");
                CloseAnimation();
                onFinish?.Invoke();
            });

            StartShowAnimation();
            if (immediately)
            {
                tween.Complete(true);
                Debug.LogError("COMPLETE IMMEDIETLY");
            }
        }

        public void Hide(IActivityTarget target, bool immediately, Action onFinish = null)
        {
            var tween = AnimationSequence;
            if (tween == null)
            {
                target.Hide();
                onFinish?.Invoke();
                return;
            }

            tween.OnRewind(() =>
            {
                Debug.LogError("LOCAL ON COMPLETE");
                CloseAnimation();
                target.Hide();
                onFinish?.Invoke();
            });

            StartHideAnimation();
            if (immediately)
            {
                tween.GotoWithCallbacks(0);
                Debug.LogError("COMPLETE IMMEDIETLY");
            }
        }

        private Tween AnimationSequence
        {
            get
            {
                if (animationTween == null)
                {
                    animationTween = showAnimationContext?.CreateAnimationTween();
                    animationTween.Pause();
                    animationTween.SetAutoKill(false);
                    //animationTween.OnPlay(BeginShowAnimation);
                    //animationTween.OnRewind(CloseAnimation);
                    //animationTween.OnComplete(CloseAnimation);
                    animationTween.Complete();
                }

                return animationTween;
            }
        }

        public bool Shows { get; private set; }
        public bool Hides { get; private set; }
        public IAnimationContext ShowAnimationContext => showAnimationContext;
        public IAnimationContext HideAnimationContext => hideAnimationContext;
    }
}