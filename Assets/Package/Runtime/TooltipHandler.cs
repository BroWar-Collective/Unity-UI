using System;
using UnityEngine;

namespace BroWar.UI
{
    [Serializable]
    public class TooltipHandler : IUiFeatureHandler
    {
        [SerializeField]
        private Canvas canvas;

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            if (!IsTooltipActive)
            {
                return;
            }

            //TODO: update tooltips
        }

        public bool IsTooltipActive { get; private set; }
    }
}