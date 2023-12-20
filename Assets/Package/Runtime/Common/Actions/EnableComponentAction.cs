using System;
using UnityEngine;

namespace BroWar.UI.Common.Actions
{
    [Serializable]
    public class EnableComponentAction : IContentRefreshAction
    {
        [SerializeField]
        private MonoBehaviour target;
        [SerializeField]
        private bool enable;

        public void Perform()
        {
            target.enabled = enable;
        }
    }
}