using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;

namespace BroWar.UI.Tooltip
{
    using BroWar.Common;

    /// <summary>
    /// Component responsible for showing/hiding tooltips on an associated UI object.
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class TooltipTarget<T> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler where T : TooltipBehaviour
    {
        [SerializeField, FormerlySerializedAs("settings")]
        private TooltipData data;
        [SerializeField]
        [Tooltip("Optional, custom tooltip prefab. If null, the default one will be used.")]
        private T customPrefab;

        private ITooltipHandler tooltipHandler;

        private void OnDisable()
        {
            ClearTooltip();
        }

        protected virtual void ShowTooltip()
        {
            var tooltip = tooltipHandler.GetInstance(customPrefab);
            if (tooltip == null)
            {
                LogHandler.Log("[UI][Tooltip] Cannot create tooltip instance.", LogType.Warning);
                return;
            }

            OnTooltipCreated(tooltip);
            tooltipHandler.ShowTooltip(tooltip, data);
        }

        protected virtual void HideTooltip()
        {
            tooltipHandler.HideTooltip();
        }

        protected abstract void OnTooltipCreated(T tooltip);

        [Inject]
        internal void Inject(ITooltipHandler tooltipHandler)
        {
            this.tooltipHandler = tooltipHandler;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            ApplyTooltip();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            ClearTooltip();
        }

        public bool ApplyTooltip()
        {
            if (!ShouldShowContent)
            {
                return false;
            }

            ShowTooltip();
            return true;
        }

        public virtual void ClearTooltip()
        {
            HideTooltip();
        }

        protected virtual bool ShouldShowContent => true;
    }
}