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

        [Line]
        [SerializeField, NotNull]
        private TooltipBehaviour tooltipPrefab;
        [SerializeField, Disable]
        [Tooltip("Tooltip instance created in runtime.")]
        private TooltipBehaviour tooltip;

        //TODO: temporary, will be handled by the HandlersManager
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

            //TODO: better way to handle input?
            //TODO: none mouse position
            var position = Mouse.current.position.ReadValue();
            tooltip.UpdatePosition(position);
        }

        public override void Dispose()
        {
            base.Dispose();
            HideTooltip();
        }

        //TODO: separate method to set content and separate to show tooltip
        //TODO: rename it to TooltipData?
        public void ShowTooltip(string contentText, in TooltipSettings settings)
        {
            tooltip.UpdateContent(contentText, in settings);
            tooltip.Show();
            tooltip.FixRectSize();
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