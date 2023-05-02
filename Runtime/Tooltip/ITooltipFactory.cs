namespace BroWar.UI.Tooltip
{
    internal interface ITooltipFactory
    {
        TooltipBehaviour Create();
        T Create<T>(T prefab) where T : TooltipBehaviour;
        void Dispose(TooltipBehaviour target);
    }
}