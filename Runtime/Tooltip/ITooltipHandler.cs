namespace BroWar.UI.Tooltip
{
    public interface ITooltipHandler
    {
        TooltipBehaviour ShowTooltip(string contentText, in TooltipData data);
        TooltipBehaviour ShowTooltip(string contentText, in TooltipData data, TooltipBehaviour tooltipPrefab);
        void HideTooltip();
    }
}