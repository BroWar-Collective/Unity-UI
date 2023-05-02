using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace BroWar.UI.Handlers
{
    using BroWar.Common;
    using BroWar.UI.Tooltip;

    /// <summary>
    /// <see cref="IUiHandler"/> responsible for showing and updating tooltips.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Handlers/Tooltip Handler")]
    public class TooltipHandler : UiHandlerBehaviour, ITooltipHandler
    {
        [SerializeField]
        private InputSystemUIInputModule inputModule;

        [Line]
        [SerializeField, SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        private ITooltipFactory factory;

        [SpaceArea]
        [SerializeField, Disable]
        [Tooltip("Currently active tooltip instance.")]
        private TooltipBehaviour activeTooltip;

        private TooltipBehaviour ActiveTooltip
        {
            get => activeTooltip;
            set
            {
                HideTooltip();
                activeTooltip = value;
            }
        }

        private Vector2 ScreenPosition
        {
            get
            {
                //TODO: UI-based interface to handle input
                //TODO: get it from the EventSystem
                var pointInputReference = inputModule.point;
                return pointInputReference.action.ReadValue<Vector2>();
            }
        }

        private void ShowTooltip(TooltipBehaviour tooltip)
        {
            if (tooltip == null)
            {
                LogHandler.Log("[UI] Cannot show tooltip. Instance is null.", LogType.Error);
                return;
            }

            ActiveTooltip = tooltip;
            ActiveTooltip.Show();
        }

        public override void Prepare()
        {
            base.Prepare();
        }

        public override void Tick()
        {
            base.Tick();
            if (!IsTooltipActive)
            {
                return;
            }

            activeTooltip.UpdatePosition(ScreenPosition);
        }

        public override void Dispose()
        {
            base.Dispose();
            HideTooltip();
        }

        public T ShowTooltip<T>(in TooltipData data) where T : TooltipBehaviour
        {
            return ShowTooltip<T>(in data, null);
        }

        public T ShowTooltip<T>(in TooltipData data, T tooltipPrefab) where T : TooltipBehaviour
        {
            var instance = factory.Create(tooltipPrefab);
            instance.UpdatePositionAndData(ScreenPosition, in data);
            ShowTooltip(instance);
            return instance;
        }

        public void HideTooltip()
        {
            if (!IsTooltipActive)
            {
                return;
            }

            factory.Dispose(ActiveTooltip);

            ActiveTooltip.Hide();
            ActiveTooltip = null;
        }

        public override bool IsTickable => IsTooltipActive;

        public bool IsTooltipActive => ActiveTooltip != null && ActiveTooltip.IsActive;
    }
}