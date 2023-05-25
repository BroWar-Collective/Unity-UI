namespace BroWar.UI.Tooltip
{
    /// <summary>
    /// Dedicated factory responsible for creating custom <see cref="TooltipBehaviour"/>s based on provided prefabs.
    /// Allows to create tooltips using different types or different prefabs of the same type.
    /// </summary>
    internal interface ITooltipFactory
    {
        TooltipBehaviour Create();
        T Create<T>(TooltipBehaviour prefab) where T : TooltipBehaviour;
        void Dispose(TooltipBehaviour target);
    }
}