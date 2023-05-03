namespace BroWar.UI.Tooltip
{
    internal interface ITooltipFactory
    {
        TooltipBehaviour Create();
        T Create<T>(TooltipBehaviour prefab) where T : TooltipBehaviour;
        void Dispose(TooltipBehaviour target);
    }
}