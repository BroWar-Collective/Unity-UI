using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BroWar.UI.Views
{
    /// <summary>
    /// Definition used to pre-initialize <see cref="UiView"/>s.
    /// </summary>
    [Serializable]
    public class ViewDefinition
    {
        [Tooltip("Indicates whether the view should be shown or hidden during initialization.")]
        public bool showOnInitialize;
        [Tooltip("Indicates whether View should be shown/hidden immediately during the initialization. Immediately flag will skip all related animations.")]
        [FormerlySerializedAs("showImmediately")]
        public bool setImmediately;
        public UiView view;
    }
}