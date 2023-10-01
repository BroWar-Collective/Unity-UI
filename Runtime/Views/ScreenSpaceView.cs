using UnityEngine;
using UnityEngine.Assertions;

namespace BroWar.UI.Views
{
    /// <summary>
    /// Canvas-based View implementation. 
    /// Typical and ready-to-use solution to provide Screen Space elements.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public abstract class ScreenSpaceView : UiView
    {
        [SerializeField, NotNull]
        private Canvas canvas;

        private CanvasGroup group;

        private void ValidateCanvas()
        {
            var renderMode = canvas.renderMode;
            var isScreenSpace = renderMode == RenderMode.ScreenSpaceOverlay || renderMode == RenderMode.ScreenSpaceCamera;
            Assert.IsTrue(isScreenSpace, $"[UI][View] Invalid Render Mode - {renderMode}.");
        }

        protected override void OnInitialize(ViewData data)
        {
            base.OnInitialize(data);
            Assert.IsNotNull(canvas, $"[UI][View] {nameof(Canvas)} is not availabe.");
            ValidateCanvas();
            var camera = data?.UiCamera;
            canvas.worldCamera = camera;
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