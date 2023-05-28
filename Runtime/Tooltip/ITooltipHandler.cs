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
        void ShowInstance(TooltipBehaviour instance, in TooltipData data);
        void ShowInstance(TooltipBehaviour instance);
        TooltipBehaviour ShowDefault(in TooltipData data);
        T ShowTooltip<T>(in TooltipData data, T tooltipPrefab) where T : TooltipBehaviour;
        /// <summary>
        /// Hides currently active <see cref="TooltipBehaviour"/>.
        /// </summary>
        void HideTooltip();

        bool IsTooltipActive { get; }
    }
}