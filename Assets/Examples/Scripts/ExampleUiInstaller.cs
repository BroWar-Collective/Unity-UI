using BroWar.UI.Management;
using UnityEngine;
using Zenject;

namespace Examples
{
    public class ExampleUiInstaller : MonoInstaller
    {
        [SerializeField]
        private UiManager handlersMananger;

        public override void InstallBindings()
        {
            var handlers = handlersMananger.Handlers;
            foreach (var handler in handlers)
            {
                if (handler == null)
                {
                    continue;
                }

                var type = handler.GetType();
                Container.BindInterfacesTo(type)
                    .FromInstance(handler)
                    .AsCached();
            }
        }
    }
}