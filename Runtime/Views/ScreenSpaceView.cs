using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
        private readonly List<UiPanel> panels = new List<UiPanel>();

        [Title("General")]
        [SerializeField, NotNull]
        private Canvas canvas;
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        private IShowHideHandler showHideHandler;

        private CanvasGroup group;

        protected override void OnInitialize(ViewData data)
        {
            base.OnInitialize(data);
            Assert.IsNotNull(canvas, $"[UI][View] {nameof(Canvas)} is not availabe.");

            panels.Clear();
            var camera = data?.CanvasCamera;
            canvas.worldCamera = camera;
        }

        protected void RegisterPanel(UiPanel panel)
        {
            panels.Add(panel);
        }

        public override void Show(bool immediately, Action onFinish = null)
        {
            if (showHideHandler == null)
            {
                base.Show(immediately, onFinish);
                return;
            }

            showHideHandler.Show(this, immediately, onFinish);
            foreach (var panel in panels)
            {
                panel.Show(immediately);
            }
        }

        public override void Hide(bool immediately, Action onFinish = null)
        {
            if (showHideHandler == null)
            {
                base.Hide(immediately, onFinish);
                return;
            }

            showHideHandler.Hide(this, immediately, onFinish);
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

        public override bool Shows => showHideHandler?.Shows ?? false;
        public override bool Hides => showHideHandler?.Hides ?? false;
    }
}