using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Management
{
    using BroWar.Common;

    /// <inheritdoc cref="IUiManager" />
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/UI Manager")]
    public class UiManager : ManagerBase, IUiManager
    {
        //NOTE: main reason to have component-based handlers is to have easier way to extended Editor features
        //this can be easily transferred into pure interface-based approach

        [SerializeField]
        private bool selfInitialize = true;
        /// <inheritdoc cref="IUiManager.Handlers" />
        [SerializeField, ReorderableList(HasLabels = false, Foldable = true), InLineEditor]
        private UiHandlerBehaviour[] handlers;

        private void Start()
        {
            if (selfInitialize)
            {
                Initialize();
            }
        }

        private void Update()
        {
            if (!IsInitialized)
            {
                return;
            }

            foreach (var handler in handlers)
            {
                if (handler == null || !handler.IsTickable)
                {
                    continue;
                }

                handler.Tick();
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
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

        protected override void OnDeinitialize()
        {
            base.OnDeinitialize();
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