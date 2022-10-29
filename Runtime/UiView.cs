using UnityEngine;

namespace BroWar.UI
{
    [RequireComponent(typeof(Canvas))]
    public class UiView : UiPanel
    {
        private Canvas canvas;

        [SerializeField, Tooltip("Nested, optional, UI panels maintained by this view.")]
        private UiPanel[] panels;

        protected override void Awake()
        {
            base.Awake();
            canvas = GetComponent<Canvas>();
        }

        public virtual void Initialize(Camera camera)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }

            canvas.worldCamera = camera;
        }

        public override void Hide()
        {
            base.Hide();
            foreach (var panel in panels)
            {
                panel.Show();
            }
        }

        public override void Show()
        {
            base.Show();
            foreach (var panel in panels)
            {
                panel.Hide();
            }
        }
    }
}