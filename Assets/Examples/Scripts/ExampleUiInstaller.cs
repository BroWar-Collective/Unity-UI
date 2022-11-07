using BroWar.UI;
using UnityEngine;
using Zenject;

namespace Examples
{
    public class ExampleUiInstaller : MonoInstaller
    {
        [SerializeField, ScriptablesList]
        private ComponentBasedHandler[] handlers;

        public override void InstallBindings()
        {
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