using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace BroWar.UI.Elements
{
    using BroWar.Common;
    using BroWar.UI.Views;

    [RequireComponent(typeof(Canvas))]
    [AddComponentMenu("BroWar/UI/Elements/UI View")]
    public class UiView : UiPanel, IInitializableWithArgument<ViewData>
    {
        [Title("General")]
        [SerializeField, NotNull]
        private Canvas canvas;

        [Title("Content")]
        [SerializeField, ReorderableList(elementLabel: "Panel")]
        [Tooltip("Nested, optional, UI panels maintained by this view.")]
        private List<UiPanel> panels = new List<UiPanel>();

        private CanvasGroup group;

        public event Action OnInitialized;

        protected void RegisterPanel(UiPanel panel)
        {
            if (panels == null)
            {
                panels = new List<UiPanel>();
            }

            panels.Add(panel);
        }

        public virtual void Initialize(ViewData data)
        {
            var camera = data?.CanvasCamera;
            Assert.IsNotNull(camera, $"[UI][View] {nameof(Camera)} is not availabe.");
            Assert.IsNotNull(canvas, $"[UI][View] {nameof(Canvas)} is not availabe.");
            canvas.worldCamera = camera;

            IsInitialized = true;
            OnInitialized?.Invoke();
        }

        public override void Show(bool immediately, Action onFinish = null)
        {
            base.Show(immediately, onFinish);
            foreach (var panel in panels)
            {
                panel.Show(immediately);
            }
        }

        public override void Hide(bool immediately, Action onFinish = null)
        {
            base.Hide(immediately, onFinish);
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

        public IReadOnlyList<UiPanel> NestedPanels => panels;

        public bool IsInitialized { get; private set; }
    }
}