namespace BroWar.UI
{
    public interface IUiFeatureHandler
    {
        void Prepare();
        void Dispose();
        void Tick();
    }
}