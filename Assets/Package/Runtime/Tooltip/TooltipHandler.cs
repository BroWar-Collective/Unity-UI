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

        private IInputHandler inputHandler;

        [Inject]
        internal void Inject(IInputHandler inputHandler)
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

        public TooltipBehaviour ShowDefault(TooltipData data)
        {
            return ShowTooltip<TooltipBehaviour>(data, null);
        }

        public T ShowTooltip<T>(TooltipData data, T tooltipPrefab) where T : TooltipBehaviour
        {
            var instance = GetInstance(tooltipPrefab);
            ShowTooltip(instance, data);
            return instance;
        }

        public void ShowTooltip(TooltipBehaviour instance)
        {
            ShowTooltip(instance, null);
        }

        public void ShowTooltip(TooltipBehaviour instance, TooltipData data)
        {
            if (instance == null)
            {
                LogHandler.Log("[UI][Tooltip] Cannot show tooltip. Instance is null.", LogType.Error);
                return;
            }

            HideTooltip();

            var position = PointerPosition;
            ActiveTooltip = instance;
            ActiveTooltip.Prepare(data);
            ActiveTooltip.UpdatePosition(position);
            ActiveTooltip.Hide(true);
            ActiveTooltip.Show(false);
        }

        public void HideTooltip()
        {
            if (!IsTooltipActive)
            {
                return;
            }

            var tooltip = ActiveTooltip;
            tooltip.Hide(false, () =>
            {
                factory.Dispose(tooltip);
            });

            ActiveTooltip = null;
        }

        private TooltipBehaviour ActiveTooltip
        {
            get => activeTooltip;
            set => activeTooltip = value;
        }

        private Vector2? PointerPosition
        {
            get => inputHandler.PointerPosition;
        }

        public override bool IsTickable => IsTooltipActive;

        public bool IsTooltipActive => ActiveTooltip != null && ActiveTooltip.IsActive;
    }
}