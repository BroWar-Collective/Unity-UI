using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
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
        [SerializeField]
        private TooltipSettings settings;
        [SerializeField, Min(0)]
        [Tooltip("Time needed to show tooltip content.")]
        private float offsetTime = 0.0f;

        private ITooltipHandler tooltipHandler;
        private Sequence sequence;

        private void ShowTooltip()
        {
            tooltipHandler.ShowTooltip(tooltipContent, in settings);
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