using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Management
{
    using BroWar.Common;

    /// <inheritdoc cref="IUiManager" />
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/UI Manager")]
    public class UiManager : StandaloneManager, IUiManager
    {
        //NOTE: main reason to have component-based handlers is to have easier way to extended Editor features
        //this can be easily transferred into pure interface-based approach

        /// <inheritdoc cref="IUiManager.Handlers" />
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

        /// <inheritdoc cref="IUiManager" />
        public IReadOnlyList<IUiHandler> Handlers => handlers;
    }
}