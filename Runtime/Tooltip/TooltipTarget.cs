using DG.Tweening;
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
        [SerializeField, Min(0)]
        [Tooltip("Time needed to show tooltip content.")]
        private float offsetTime = 0.0f;
        [SerializeField]
        [Tooltip("Optional, custom tooltip prefab. If null, the default one will be used.")]
        private T customPrefab;

        private ITooltipHandler tooltipHandler;
        private Sequence sequence;

        protected virtual void ShowTooltip()
        {
            var tooltip = tooltipHandler.GetInstance(customPrefab);
            if (tooltip == null)
            {
                LogHandler.Log("[UI][Tooltip] Cannot create tooltip instance.", LogType.Warning);
                return;
            }

            UpdateContent(tooltip);
            tooltipHandler.ShowInstance(tooltip, in data);
        }

        protected virtual void HideTooltip()
        {
            tooltipHandler.HideTooltip();
        }

        protected abstract void UpdateContent(T tooltip);

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

            sequence = DOTween.Sequence();
            sequence.AppendInterval(offsetTime);
            sequence.AppendCallback(ShowTooltip);
            return false;
        }

        public void ClearTooltip()
        {
            sequence?.Kill();
            sequence = null;
            HideTooltip();
        }

        protected virtual bool ShouldShowContent => sequence == null || sequence.IsComplete();
    }
}