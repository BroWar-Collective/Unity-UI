﻿using System;
using DG.Tweening;
using UnityEngine;

namespace BroWar.UI.Animation.Animations
{
    [Serializable]
    public class SlideInAnimationContext : IAnimationContext
    {
        [SerializeField]
        private RectTransform transform;
        [SerializeField]
        private AnimationDirection direction;
        [SerializeField, SearchableEnum]
        private Ease ease = Ease.OutCubic;
        [SerializeField, Min(0)]
        private float duration = 1.0f;

        public Sequence GetSequence()
        {
            return AnimationUtility.SlideIn(transform, direction, ease, duration);
        }
    }
}