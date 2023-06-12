using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace BroWar.UI.Views
{
    using BroWar.UI.Common;

    [RequireComponent(typeof(Canvas))]
    [AddComponentMenu("BroWar/UI/Views/UI View (Screen Space)")]
    public class ScreenSpaceView : UiView
    {
        [Title("General")]
        [SerializeField, NotNull]
        private Canvas canvas;
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        private IShowHideHandler showHideHandler;

        [Title("Content")]
        [SerializeField]
        private UiPanel mainPanel;
        [SerializeField, ReorderableList(elementLabel: "Panel")]
        [Tooltip("Nested, optional, UI panels maintained by this view.")]
        private List<UiPanel> panels = new List<UiPanel>();

        private CanvasGroup group;

        protected override void OnInitialize(ViewData data)
        {
            base.OnInitialize(data);
            var camera = data?.CanvasCamera;
            Assert.IsNotNull(camera, $"[UI][View] {nameof(Camera)} is not availabe.");
            Assert.IsNotNull(canvas, $"[UI][View] {nameof(Canvas)} is not availabe.");
            canvas.worldCamera = camera;
        }

        protected void RegisterPanel(UiPanel panel)
        {
            if (panels == null)
            {
                panels = new List<UiPanel>();
            }

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
            foreach (var panel in NestedPanels)
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
            foreach (var panel in NestedPanels)
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

        public IReadOnlyList<UiPanel> NestedPanels => panels;

        public override bool Shows => showHideHandler?.Shows ?? false;
        public override bool Hides => showHideHandler?.Hides ?? false;
    }
}