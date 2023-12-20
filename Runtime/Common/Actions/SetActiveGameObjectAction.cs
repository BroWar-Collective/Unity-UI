using System;
using UnityEngine;

namespace BroWar.UI.Common.Actions
{
    [Serializable]
    public class SetActiveGameObjectAction : IContentRefreshAction
    {
        [SerializeField]
        private GameObject target;
        [SerializeField]
        private bool active;

        public void Perform()
        {
            target.SetActive(active);
        }
    }
}