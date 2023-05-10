using UnityEngine;
using UnityEngine.Assertions;

namespace BroWar.UI.Elements
{
    [RequireComponent(typeof(Canvas))]
    [AddComponentMenu("BroWar/UI/Elements/UI View")]
    public class UiView : UiPanel
    {
        [Title("General")]
        [SerializeField]
        private Canvas canvas;
        [SerializeField, ReorderableList, Tooltip("Nested, optional, UI panels maintained by this view.")]
        private UiPanel[] panels;

        private CanvasGroup group;

        public virtual void Initialize(Camera camera)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }

            Assert.IsNotNull(canvas, $"[UI][View] {nameof(Canvas)} reference is not availabe.");
            canvas.worldCamera = camera;
        }

        public override void Show()
        {
            base.Show();
            foreach (var panel in panels)
            {
                panel.Hide();
            }
        }

        public override void Hide()
        {
            base.Hide();
            foreach (var panel in panels)
            {
                panel.Show();
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
    }
}