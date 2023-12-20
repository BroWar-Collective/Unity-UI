using System;
using UnityEngine;

namespace BroWar.UI.Common.Actions
{
    [Serializable]
    public class SetActiveObjectAction : IContentRefreshAction
    {
        [SerializeField]
        private UiObject target;
        [SerializeField]
        private bool active;

        public void Perform()
        {
            target.SetActive(active);
        }
    }
}