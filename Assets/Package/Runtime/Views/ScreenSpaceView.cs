using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace BroWar.UI.Views
{
    using BroWar.UI.Elements;

    [RequireComponent(typeof(Canvas))]
    [AddComponentMenu("BroWar/UI/Elements/UI View (Screen Space)")]
    public class ScreenSpaceView : UiView
    {
        //TODO: show/hide handler

        [Title("General")]
        [SerializeField, NotNull]
        private Canvas canvas;

        [Title("Content")]
        [SerializeField]
        private UiPanel mainPanel;
        [SerializeField, ReorderableList(elementLabel: "Panel")]
        [Tooltip("Nested, optional, UI panels maintained by this view.")]
        private List<UiPanel> panels = new List<UiPanel>();

        private CanvasGroup group;

        protected override void OnInitialize(ViewData data)
        {
            Assert.IsNotNull(mainPanel);

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
            base.Show(immediately, null);
            mainPanel.Show(immediately, onFinish);
            foreach (var panel in panels)
            {
                panel.Show(immediately);
            }
        }

        public override void Hide(bool immediately, Action onFinish = null)
        {
            base.Hide(immediately, null);
            mainPanel.Hide(immediately, onFinish);
            foreach (var panel in panels)
            {
                panel.Hide(immediately);
            }
        }

        public override bool CanShow()
        {
            return base.CanShow() || mainPanel.Hides;
        }

        public override bool CanHide()
        {
            return base.CanHide() && !mainPanel.Hides;
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
    }
}