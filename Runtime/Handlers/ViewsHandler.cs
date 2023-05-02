using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Handlers
{
    using BroWar.UI.Elements;

    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Handlers/Views Handler")]
    public class ViewsHandler : UiHandlerBehaviour
    {
        [SerializeField, ReorderableList]
        private List<UiView> views;
    }
}