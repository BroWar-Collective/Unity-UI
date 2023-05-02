using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;

namespace BroWar.UI.Tooltip
{
    /// <summary>
    /// Component responsible for showing/hiding tooltips on an associated UI object.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Tooltip/Tooltip Target")]
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
            var tooltip = tooltipHandler.ShowTooltip(in data, customPrefab);
            if (tooltip != null)
            {
                UpdateContent(tooltip);
            }
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
            if (!ShouldShowContent)
            {
                return;
            }

            sequence = DOTween.Sequence();
            sequence.AppendInterval(offsetTime);
            sequence.AppendCallback(ShowTooltip);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            sequence?.Kill();
            HideTooltip();
        }

        protected virtual bool ShouldShowContent => true;
    }
}