using UnityEngine;

namespace BroWar.UI.Management
{
    public class UiHandlersManager
    {
        [SerializeField, SerializeReference, ReferencePicker]
        private IUiFeatureHandler[] handlers;

        //TODO: inject handlers
    }
}