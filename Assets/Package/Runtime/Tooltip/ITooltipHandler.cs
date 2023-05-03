namespace BroWar.UI.Tooltip
{
    public interface ITooltipHandler
    {
        TooltipBehaviour ShowTooltip(in TooltipData data);
        T ShowTooltip<T>(in TooltipData data, T tooltipPrefab) where T : TooltipBehaviour;
        void HideTooltip();

        bool IsTooltipActive { get; }
    }
}