using System;
using UnityEngine;

namespace BroWar.UI.Handlers
{
    [Serializable]
    public class TooltipHandler : MonoBehaviour, IUiFeatureHandler
    {
        [SerializeField]
        private Canvas canvas;

        public void Prepare()
        { }

        public void Dispose()
        { }

        public void Tick()
        {
            //TODO: update tooltips
        }

        public bool IsTooltipActive { get; private set; }
        public bool IsTickable => IsTooltipActive;
    }
}