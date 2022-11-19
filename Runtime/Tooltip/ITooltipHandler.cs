namespace BroWar.UI.Tooltip
{
    public interface ITooltipHandler
    {
        void ShowTooltip(string contentText, in TooltipSettings settings);
        void HideTooltip();
    }
}