using UnityEngine;

namespace BroWar.UI
{
    public class PopupHandler : MonoBehaviour, IUiFeatureHandler
    {
        public void Prepare()
        { }

        public void Dispose()
        { }

        public void Tick()
        { }

        public bool IsTickable => false;
    }
}