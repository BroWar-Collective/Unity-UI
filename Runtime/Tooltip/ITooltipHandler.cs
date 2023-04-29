namespace BroWar.UI.Tooltip
{
    public interface ITooltipHandler
    {
        void ShowTooltip(string contentText, in TooltipData data);
        TooltipBehaviour ShowTooltip(string contentText, in TooltipData data, TooltipBehaviour tooltipPrefab);
        void HideTooltip();
    }
}