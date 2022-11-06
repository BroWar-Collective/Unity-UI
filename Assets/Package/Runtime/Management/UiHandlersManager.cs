using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Management
{
    [DisallowMultipleComponent]
    public class UiHandlersManager : MonoBehaviour
    {
        [SerializeField, SerializeReference, ReferencePicker, ReorderableList]
        private IUiFeatureHandler[] handlers;

        private void OnEnable()
        {
            for (var i = 0; i < handlers.Length; i++)
            {
                var handler = handlers[i];
                handler.Prepare();
            }
        }

        private void Update()
        {
            for (var i = 0; i < handlers.Length; i++)
            {
                var handler = handlers[i];
                if (handler.IsTickable)
                {
                    handler.Tick();
                }
            }
        }

        private void OnDisable()
        {
            for (var i = 0; i < handlers.Length; i++)
            {
                var handler = handlers[i];
                handler.Dispose();
            }
        }

        //TODO:
        public bool TryGetHandler()
        {
            return false;
        }

        public IReadOnlyList<IUiFeatureHandler> Handlers => handlers;
    }
}