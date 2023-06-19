using UnityEngine;
using Zenject;

namespace BroWar.UI.Tooltip
{
    using BroWar.Common;
    using BroWar.UI.Input;

    /// <inheritdoc cref="ITooltipHandler"/>
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Tooltip/Tooltip Handler")]
    public class TooltipHandler : UiHandlerBehaviour, ITooltipHandler
    {
        [SerializeField, SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        private ITooltipFactory factory;

        [SpaceArea, Line]

        [SerializeField, Disable]
        [Tooltip("Currently active tooltip instance.")]
        private TooltipBehaviour activeTooltip;

        private IUiInputHandler inputHandler;

        private void ShowTooltip(TooltipBehaviour tooltip)
        {
            if (tooltip == null)
            {
                LogHandler.Log("[UI][Tooltip] Cannot show tooltip. Instance is null.", LogType.Error);
                return;
            }

            ActiveTooltip = tooltip;
            ActiveTooltip.Show();
        }

        [Inject]
        internal void Inject(IUiInputHandler inputHandler)
        {
            this.inputHandler = inputHandler;
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

            var position = PointerPosition;
            activeTooltip.UpdatePosition(position);
        }

        public override void Dispose()
        {
            base.Dispose();
            HideTooltip();
        }

        public T GetInstance<T>(T tooltipPrefab) where T : TooltipBehaviour
        {
            return factory.Create<T>(tooltipPrefab);
        }

        public void ShowInstance(TooltipBehaviour instance, in TooltipData data)
        {
            var position = PointerPosition;
            instance.UpdatePositionAndData(position, in data);
            ShowInstance(instance);
        }

        public void ShowInstance(TooltipBehaviour instance)
        {
            ShowTooltip(instance);
        }

        public TooltipBehaviour ShowDefault(in TooltipData data)
        {
            return ShowTooltip<TooltipBehaviour>(in data, null);
        }

        public T ShowTooltip<T>(in TooltipData data, T tooltipPrefab) where T : TooltipBehaviour
        {
            var instance = GetInstance(tooltipPrefab);
            ShowInstance(instance, in data);
            return instance;
        }

        public void HideTooltip()
        {
            if (!IsTooltipActive)
            {
                return;
            }

            ActiveTooltip.Hide();
            factory.Dispose(ActiveTooltip);
            ActiveTooltip = null;
        }

        private TooltipBehaviour ActiveTooltip
        {
            get => activeTooltip;
            set
            {
                HideTooltip();
                activeTooltip = value;
            }
        }

        private Vector2? PointerPosition
        {
            get => inputHandler.PointerPosition;
        }

        public override bool IsTickable => IsTooltipActive;

        public bool IsTooltipActive => ActiveTooltip != null && ActiveTooltip.IsActive;
    }
}