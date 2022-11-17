namespace BroWar.UI.Tooltip
{
    public interface ITooltipHandler
    {
        void ShowTooltip(string contentText);
        void HideTooltip();
    }
}