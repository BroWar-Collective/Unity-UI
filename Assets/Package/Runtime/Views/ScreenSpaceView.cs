﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace BroWar.UI.Views
{
    using BroWar.UI.Common;

    /// <summary>
    /// Canvas-based View implementation. 
    /// Typical and ready-to-use solution to provide Screen Space elements.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public abstract class ScreenSpaceView : UiView
    {
        //NOTE: consider using contexts to have "metadata" about panels
        private readonly List<UiPanel> panels = new List<UiPanel>();

        [Title("General")]
        [SerializeField, NotNull]
        private Canvas canvas;
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        [FormerlySerializedAs("showHideHandler")]
        private IActivityHandler activityHandler;

        private CanvasGroup group;

        protected override void OnInitialize(ViewData data)
        {
            base.OnInitialize(data);
            Assert.IsNotNull(canvas, $"[UI][View] {nameof(Canvas)} is not availabe.");
            var renderMode = canvas.renderMode;
            var isScreenSpace = renderMode == RenderMode.ScreenSpaceOverlay || renderMode == RenderMode.ScreenSpaceCamera;
            Assert.IsTrue(isScreenSpace, $"[UI][View] Invalid Render Mode - {renderMode}.");

            panels.Clear();
            var camera = data?.UiCamera;
            canvas.worldCamera = camera;
        }

        protected void RegisterPanel(UiPanel panel)
        {
            panels.Add(panel);
        }

        public override void Show(bool immediately, Action onFinish = null)
        {
            if (activityHandler == null)
            {
                base.Show(immediately, onFinish);
                return;
            }

            activityHandler.Show(this, immediately, onFinish);
            foreach (var panel in panels)
            {
                panel.Show(immediately);
            }
        }

        public override void Hide(bool immediately, Action onFinish = null)
        {
            if (activityHandler == null)
            {
                base.Hide(immediately, onFinish);
                return;
            }

            activityHandler.Hide(this, immediately, onFinish);
            foreach (var panel in panels)
            {
                panel.Hide(immediately);
            }
        }

        public CanvasGroup Group
        {
            get
            {
                if (group == null)
                {
                    group = GetComponent<CanvasGroup>();
                }

                return group;
            }
        }

        public IReadOnlyList<UiPanel> Panels => panels;
        public override bool Shows => activityHandler?.Shows ?? false;
        public override bool Hides => activityHandler?.Hides ?? false;
    }
}