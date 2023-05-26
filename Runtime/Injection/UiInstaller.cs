using System;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace BroWar.UI.Injection
{
    using BroWar.Injection;
    using BroWar.UI.Management;

    [Serializable]
    public class UiInstaller : ExposableSubInstaller
    {
        [SerializeField]
        private UiManager manager;

        protected override void OnInstall(DiContainer container)
        {
            Assert.IsNotNull(manager, $"[UI][Injection] {nameof(UiManager)} not available.");
            var handlers = manager.Handlers;
            foreach (var handler in handlers)
            {
                if (handler == null)
                {
                    continue;
                }

                var type = handler.GetType();
                container.BindInterfacesTo(type)
                    .FromInstance(handler)
                    .AsCached();
                container.QueueForInject(handler);
            }
        }
    }
}