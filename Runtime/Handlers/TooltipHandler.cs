using UnityEngine;
using UnityEngine.InputSystem;

namespace BroWar.UI.Handlers
{
    using BroWar.UI.Tooltip;

    [AddComponentMenu("BroWar/UI/Handler/Tooltip Handler")]
    public class TooltipHandler : UiHandlerBehaviour, ITooltipHandler
    {
        [SerializeField]
        private Canvas canvas;
        [SerializeField, NotNull]
        private TooltipBehaviour tooltipPrefab;
        [SerializeField, Disable]
        [Tooltip("Tooltip instance created in runtime.")]
        private TooltipBehaviour tooltip;

        //TODO: temporary
        private void OnEnable()
        {
            Prepare();
        }

        private void Update()
        {
            Tick();
        }

        private void OnDisable()
        {
            Dispose();
        }

        public override void Prepare()
        {
            base.Prepare();
            tooltip = Instantiate(tooltipPrefab, CanvasParent);
            tooltip.name = tooltipPrefab.name;
            HideTooltip();
        }

        public override void Tick()
        {
            if (!IsTooltipActive)
            {
                return;
            }

            var position = Mouse.current.position.ReadValue();
            tooltip.UpdateRect(position);
        }

        public override void Dispose()
        {
            base.Dispose();
            HideTooltip();
        }

        public void ShowTooltip(string contentText)
        {
            tooltip.UpdateText(contentText);
            tooltip.Show();
        }

        public void HideTooltip()
        {
            if (tooltip != null && tooltip.IsActive)
            {
                tooltip.Hide();
            }
        }

        private Transform CanvasParent => canvas.transform;

        public override bool IsTickable => IsTooltipActive;
        public bool IsTooltipActive => tooltip != null && tooltip.IsActive;
    }
}