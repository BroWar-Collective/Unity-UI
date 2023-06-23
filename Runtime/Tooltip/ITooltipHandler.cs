namespace BroWar.UI.Tooltip
{
    /// <summary>
    /// <see cref="IUiHandler"/> responsible for showing and updating tooltips.
    /// </summary>
    public interface ITooltipHandler
    {
        /// <summary>
        /// Returns runtime <see cref="TooltipBehaviour"/> instance based on the given prefab.
        /// </summary>
        T GetInstance<T>(T tooltipPrefab) where T : TooltipBehaviour;
        TooltipBehaviour ShowDefault(TooltipData data);
        T ShowTooltip<T>(TooltipData data, T tooltipPrefab) where T : TooltipBehaviour;
        void ShowTooltip(TooltipBehaviour instance, TooltipData data);
        void ShowTooltip(TooltipBehaviour instance);
        /// <summary>
        /// Hides currently active <see cref="TooltipBehaviour"/>.
        /// </summary>
        void HideTooltip();

        bool IsTooltipActive { get; }
    }
}