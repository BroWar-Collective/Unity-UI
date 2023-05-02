namespace BroWar.UI.Tooltip
{
    public interface ITooltipHandler
    {
        T ShowTooltip<T>(in TooltipData data) where T : TooltipBehaviour;
        T ShowTooltip<T>(in TooltipData data, T tooltipPrefab) where T : TooltipBehaviour;
        //TooltipBehaviour ShowTooltip(string contentText, in TooltipData data);
        //TooltipBehaviour ShowTooltip(string contentText, in TooltipData data, TooltipBehaviour tooltipPrefab);
        void HideTooltip();
    }
}