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
    public class TooltipTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, TextArea(4, 8)]
        private string tooltipContent;
        [SerializeField, FormerlySerializedAs("settings")]
        private TooltipData data;
        [SerializeField, Min(0)]
        [Tooltip("Time needed to show tooltip content.")]
        private float offsetTime = 0.0f;
        [SerializeField]
        [Tooltip("Optional, custom prefab. If null the default one will be used.")]
        private TooltipBehaviour customTooltip;

        private ITooltipHandler tooltipHandler;
        private Sequence sequence;

        private void ShowTooltip()
        {
            tooltipHandler.ShowTooltip(tooltipContent, in data, customTooltip);
        }

        private void HideTooltip()
        {
            tooltipHandler.HideTooltip();
        }

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

        private bool ShouldShowContent => !string.IsNullOrEmpty(tooltipContent);
    }
}