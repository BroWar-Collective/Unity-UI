using BroWar.UI.Management;
using UnityEngine;

namespace BroWar.UI.Handlers
{
    public class PopupHandler : MonoBehaviour, IUiFeatureHandler
    {
        [SerializeField]
        private ViewsManager viewsManager;

        public void Prepare()
        { }

        public void Dispose()
        { }

        public void Tick()
        { }

        public bool IsTickable => false;
    }
}