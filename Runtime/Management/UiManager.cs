using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Management
{
    using BroWar.Common;

    //TODO: interface
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/UI Manager")]
    public class UiManager : StandaloneManager
    {
        [SerializeField, ReorderableList(HasLabels = false, Foldable = true)]
        private UiHandlerBehaviour[] handlers;

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

        /// <summary>
        /// Collection of all available <see cref="IUiHandler"/>s.
        /// Each handler is responsible for custom, UI-related feature.
        /// </summary>
        public IReadOnlyList<IUiHandler> Handlers => handlers;
    }
}