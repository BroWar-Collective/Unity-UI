using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Management
{
    using BroWar.Common;

    [DisallowMultipleComponent]
    public class UiHandlersManager : StandaloneManager
    {
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName), ReorderableList]
        private IUiHandler[] handlers;

        private void OnEnable()
        {
            foreach (var handler in handlers)
            {
                if (handler == null)
                {
                    LogHandler.Log("[UI] One of assigned UI handlers is null", LogType.Warning);
                    continue;
                }

                handler.Prepare();
            }
        }

        private void Update()
        {
            foreach (var handler in handlers)
            {
                if (handler == null || !handler.IsTickable)
                {
                    continue;
                }

                handler.Tick();
            }
        }

        private void OnDisable()
        {
            foreach (var handler in handlers)
            {
                if (handler == null)
                {
                    continue;
                }

                handler.Dispose();
            }
        }

        public IReadOnlyList<IUiHandler> Handlers => handlers;
    }
}